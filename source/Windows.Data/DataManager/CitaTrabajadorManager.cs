using ACME.Data.Contracts;
using ACME.Data.Entity;
using Dapper;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Data;
using System;
using System.Linq;
using System.Collections;
using Newtonsoft.Json.Linq;
//using Newtonsoft.Json;
using Nancy.Json;
using Microsoft.Extensions.Logging;

namespace ACME.Data.DataManager
{
    public class CitaTrabajadorManager : DbFactoryBase, ICitaTrabajadorManager
    {
        private readonly IConfiguration _configuration;
        private readonly int iIdUsuarioCreacion;
        private readonly ILogger<CitaTrabajadorManager> _logger;


        public async Task<object> UpdateFilemedioCompleto(string vcCodEpisodioSync)
        {
            var param = new
            {
                @vcCodEpisodioSync
            };

            return await DbExecuteAsync<object>("sproc_job_episodio_update_filemediocompleto", param, true);
        }

        public async Task<long> InsertaDocumento(DocFin doc)
        {
            var param = new
            {
                @vcNombre = doc.vcNombre,
                @vcRuta = doc.vcRuta,
                @siTipoDoc = 1000,
                @iIdSubClaseDocumento = doc.iIdSubClaseDocumento,
                @iIdUsuarioCreacion = doc.idUsuario,
                @siEstado = 1,
                @nvGUID = doc.nvGUID,
                @vcExtension = doc.vcExtension
            };

            return await DbQuerySingleAsync<long>("sproc_DOCUMENTOInsert_Cloud", param, true);
        }

        public async Task<long> InsertaEntregableaEpisodio(DocFin doc, long docid, string codEpisodio)
        {
            var iIdProcedimientoEra = await SelectIdProcedimiento(codEpisodio);
            var param = new
            {
                @iIdProcedimientoEra = iIdProcedimientoEra,
                @iIdPrueba = (int?)null,
                @iIdDocumento = docid,
                @iIdUsuarioCreacion = doc.idUsuario,
                @iIdSubClaseDocumento = doc.iIdSubClaseDocumento,
                @vcRuta = doc.vcRuta,
                @estado= "A",
                @codigoAMCDOC = ""
            };

            return await DbQuerySingleAsync<long>("sproc_PROCEDIMIENTO_ENTREGABLEInsert_Cloud", param, true);
        }

        private async Task<long?> SelectIdProcedimiento(string codEpisodio)
        {
            return await DbQuerySingleAsync<long?>("sproc_mq_episodio_select_idprocedimiento", new { @vcCodEpisodioSync = codEpisodio }, true);
        }

        public CitaTrabajadorManager(IConfiguration configuration, ILogger<CitaTrabajadorManager> logger) : base(configuration, "SQLDBConnectionString", logger)
        {
            _configuration = configuration;
            iIdUsuarioCreacion = int.Parse(@_configuration["WORKER_CitaServices:IdUsuario"]);
            _logger = logger;
        }

        public async Task<IEnumerable<EpisodioColaMerge>> ListEpisodiosColaMerge()
        {
            return await DbQueryAsync<EpisodioColaMerge>("sproc_job_episodios_cola_merge_pdf", null, true);
        }

        public async Task<IEnumerable<EpisodioEntregableMerge>> ListEpisodiosEntregables(string vcCodEpisodioSync)
        {
            var param = new
            {
                @vcCodEpisodioSync
            };

            return await DbQueryAsync<EpisodioEntregableMerge>("sproc_job_episodio_entregables", param, true);
        }

        public async Task<CertificadoDigital> GetEpisodiosEntregableDigitalSignature(string vcCodEpisodioSync,int iIdSubClaseDocumento)
        {
            var param = new
            {
                @vcCodEpisodioSync,
                @iIdSubClaseDocumento

            };

            return await DbQuerySingleAsync<CertificadoDigital>("sproc_entregable_getFirmaDigital", param, true);
        }

        public async Task<IEnumerable<CitaTrabajadorData>> ListCitaTrabajadorData()
        {
            return await DbQueryAsync<CitaTrabajadorData>("sproc_JOB_CITA_GetData", null, true);
        }

        public async Task<object> SincronizaCitaData()
        {
            var citas = await DbQueryAsync<CitaTrabajadorData>("sproc_JOB_CITA_GetData", null, true);

            foreach (var cita in citas)
            {
                if (!string.IsNullOrEmpty(cita.JsonData) && cita.IIdPaciente > 0)
                {
                    try
                    {
                        var jsonData = Newtonsoft.Json.JsonConvert.DeserializeObject<JsonData>(cita.JsonData);

                        await RegistraData(jsonData, cita.IIdCitaTrabajador, cita.IIdPaciente, cita.IIdSede);
                    }
                    catch (Exception ex)
                    {
                        _logger.Log(LogLevel.Error, ex, "Error al intentar ejecutar SincronizaCitaData.");
                        throw;
                    }
                }
                else
                {
                    _logger.Log(LogLevel.Warning, $"La cita {cita.IIdCitaTrabajador} no tiene JsonData");
                }
            }

            return true;
        }

        internal async Task RegistraData(JsonData jsonData, int iIdCitaTrabajador, int iIdPaciente, int iIdSede)
        {
            using IDbConnection dbCon = DbConnection;
            dbCon.Open();

            using var transaction = dbCon.BeginTransaction();
            //try
            {

                var pruebas = new List<Prueba>();

                foreach (var protocolo in jsonData.Protocolos)
                {
                    foreach (var prueba in protocolo.Pruebas)
                    {
                        if (pruebas.Where(x => x.IIdPrueba == prueba.IIdPrueba).Count() == 0)
                        {
                            pruebas.Add(prueba);
                        }
                    }
                }

                long iIdConcepto = await RegistraConcepto(iIdCitaTrabajador, dbCon, transaction);

                await RegistraProcedimientoArray(iIdConcepto, iIdPaciente, iIdSede, pruebas, dbCon, transaction);

                await RegistraVisitaArray(iIdConcepto, iIdPaciente, jsonData.Visitas, dbCon, transaction);

                await FinalizaRigistroCitaData(iIdCitaTrabajador, dbCon, transaction);

                transaction.Commit();
            }
            //catch (Exception ex)
            //{
            //    transaction.Rollback();
            //    _logger.Log(LogLevel.Error, ex, "Error when trying to RegistraData.");
            //    throw;
            //}
        }


        /*
         1.- CONCEPTO
         */
        internal async Task<long> RegistraConcepto(int iIdCitaTrabajador, IDbConnection dbCon, IDbTransaction transaction)
        {
            var param = new
            {
                @iIdCitaTrabajador,
                @iIdUsuarioCreacion
            };

            return await dbCon.QueryFirstOrDefaultAsync<long>("sproc_JOB_CITA_RegistraConcepto", param, commandType: CommandType.StoredProcedure, transaction: transaction);
        }

        /*
         2.- PROCEDIMIENTO
         */
        internal async Task RegistraProcedimientoArray(long iIdConcepto, int iIdPaciente, int iIdSede, IList<Prueba> pruebas, IDbConnection dbCon, IDbTransaction transaction)
        {

            foreach (var prueba in pruebas)
            {
                if (prueba.BCompletado == true)
                {
                    var param = new
                    {
                        @iIdConcepto,
                        @iIdPrueba = prueba.IIdPrueba,
                        @iIdPaciente,
                        @dtEraInicio = prueba.DtFinRegistroDatos,
                        @dtEraFin = prueba.DtFinRegistroDatos,
                        @iIdPorQuien = prueba.IIdUsuarioInicioRegistroDatos,
                        @iIdDonde = iIdSede,
                        @iIdUsuarioCreacion
                    };

                    var iIdProcedimientoEra = await dbCon.QueryFirstOrDefaultAsync<long>("sproc_JOB_CITA_RegistraProcedimiento", param, commandType: CommandType.StoredProcedure, transaction: transaction);

                    await RegistraProcedimientoAnotacion(iIdProcedimientoEra, prueba, dbCon, transaction);

                    //await RegistraCondicionArray(iIdConcepto, iIdPaciente, iIdSede, prueba.Dignosticos, dbCon, transaction);
                }
                await RegistraCondicionArray(iIdConcepto, iIdPaciente, iIdSede, prueba.IIdPrueba, prueba.Diagnosticos, dbCon, transaction);
            }
        }

        /*
         3.- PROCEDIMIENTO ANOTACION
         */
        internal async Task RegistraProcedimientoAnotacion(long iIdProcedimientoEra, Prueba prueba, IDbConnection dbCon, IDbTransaction transaction)
        {
            var param = new
            {
                @iIdProcedimientoEra,
                @iIdPrueba = prueba.IIdPrueba,
                @vcDescripcion = prueba.VcNombre,
                @iIdUsuarioCreacion
            };

            var iIdProcedimiento = await dbCon.QueryFirstOrDefaultAsync<long>("sproc_JOB_CITA_RegistraProcedimientoAnotacion", param, commandType: CommandType.StoredProcedure, transaction: transaction);

            await RegistraProcedimientoFichaArray(iIdProcedimiento, prueba.Fichas, dbCon, transaction);

            await RegistraProcedimientoEntregableArray(iIdProcedimiento, prueba.EntregablesGenerados, dbCon, transaction);
        }

        /*
         4.- PROCEDIMIENTO FICHA
         */
        internal async Task RegistraProcedimientoFichaArray(long iIdProcedimiento, IList<Ficha> fichas, IDbConnection dbCon, IDbTransaction transaction)
        {
            foreach (var ficha in fichas)
            {
                var param = new
                {
                    @iIdProcedimiento,
                    @iIdFicha = ficha.IIdFicha,
                    @vcNombre = ficha.VcNombre,
                    @vcDescripcion = ficha.VcDescripcion,
                    @iIdUsuarioCreacion
                };

                var iIdProcedimientoFicha = await dbCon.QueryFirstOrDefaultAsync<long>("sproc_JOB_CITA_RegistraProcedimientoFicha", param, commandType: CommandType.StoredProcedure, transaction: transaction);

                await RegistraProcedimientoCampoArray(iIdProcedimientoFicha, ficha.Campos, dbCon, transaction);
            }
        }

        /*
         5 y 11.- PROCEDIMIENTO CAMPO
         */
        internal async Task RegistraProcedimientoCampoArray(long iIdProcedimientoFicha, IList<Campo> campos, IDbConnection dbCon, IDbTransaction transaction)
        {
            foreach (var campo in campos)
            {
                //if (!string.IsNullOrEmpty(campo.VcNombre) && campo.ITipoValor.HasValue && !string.IsNullOrEmpty(campo.VcValor))
                if (!string.IsNullOrEmpty(campo.VcNombre) && campo.ITipoValor.HasValue && campo.VcValor != null && !string.IsNullOrEmpty(Convert.ToString(campo.VcValor)))
                {
                    try
                    {
                        var param = new
                        {
                            @iIdProcedimientoFicha,
                            @iIdCampo = campo.IIdCampo,
                            @vcNombre = campo.VcNombre,
                            //@iIdDocumento = campo.ITipoValor == 1 ? int.Parse(campo.VcValor) : (int?)null,
                            @iIdDocumento = campo.ITipoValor == 1 ? Convert.ToInt32(campo.VcValor) : (int?)null,
                            @vcValor = campo.ITipoValor == 2 ? Convert.ToString(campo.VcValor) : null,
                            //@deValor = campo.ITipoValor == 3 ? decimal.Parse(campo.VcValor) : (decimal?)null,
                            @deValor = campo.ITipoValor == 3 ? Convert.ToDecimal(campo.VcValor) : (decimal?)null,
                            //@dtValor = campo.ITipoValor == 4 ? DateTime.Parse(campo.VcValor) : (DateTime?)null,
                            @dtValor = campo.ITipoValor == 4 ? Convert.ToDateTime(campo.VcValor) : (DateTime?)null,
                            @jsonValor = campo.ITipoValor == 5 ? Convert.ToString(campo.VcValor) : "",
                            @iIdUsuarioCreacion
                        };

                        await dbCon.QueryFirstOrDefaultAsync<long>("sproc_JOB_CITA_RegistraProcedimientoCampo", param, commandType: CommandType.StoredProcedure, transaction: transaction);
                    }
                    catch (Exception ex)
                    {
                        _logger.Log(LogLevel.Error, ex, $"Error al intentar ejecutar RegistraProcedimientoCampoArray IIdCampo: {campo.IIdCampo}");
                        throw;
                    }

                }
            }
        }

        /*
         6.- PROCEDIMIENTO ENTREGABLE
         */
        internal async Task RegistraProcedimientoEntregableArray(long iIdProcedimiento, IList<Entregable> entregables, IDbConnection dbCon, IDbTransaction transaction)
        {
            foreach (var entregable in entregables)
            {
                var param = new
                {
                    @iIdProcedimiento,
                    @iIdDocumento = entregable.IIdDocumento,
                    @iIdUsuarioCreacion
                };

                await dbCon.QueryFirstOrDefaultAsync<long>("sproc_JOB_CITA_RegistraProcedimientoEntregable", param, commandType: CommandType.StoredProcedure, transaction: transaction);
            }
        }

        /*
         7.- VISITA
         */
        internal async Task RegistraVisitaArray(long iIdConcepto, int iIdPaciente, IList<Visita> visitas, IDbConnection dbCon, IDbTransaction transaction)
        {
            foreach (var visita in visitas)
            {
                if((visita.DtInicio.HasValue && visita.IIdUsuarioInicio.HasValue) 
                    || (visita.DtFin.HasValue && visita.IIdUsuarioFin.HasValue))
                {
                    var param = new
                    {
                        @iIdConcepto,
                        @iIdPaciente,
                        @dtEraInicio = visita.DtInicio,
                        @dtEraFin = visita.DtFin,
                        @iIdUsuarioInicio = visita.IIdUsuarioInicio,
                        @iIdUsuarioFin = visita.IIdUsuarioFin,
                        @iIdUsuarioCreacion
                    };

                    await dbCon.QueryFirstOrDefaultAsync<long>("sproc_JOB_CITA_RegistraVisita", param, commandType: CommandType.StoredProcedure, transaction: transaction);
                }
            }
        }

        /*
         8.- CONDICION
         */
        internal async Task RegistraCondicionArray(long iIdConcepto, int iIdPaciente, int iIdSede, int iIdPrueba, IList<Diagnostico> diagnosticos, IDbConnection dbCon, IDbTransaction transaction)
        {            
            foreach (var diagnostico in diagnosticos)
            {
                diagnostico.IIdPrueba = iIdPrueba;

                if (diagnostico.DtCreacion.HasValue && diagnostico.IIdUsuarioCreacion.HasValue)
                {
                    var param = new
                    {
                        @iIdConcepto,
                        @iIdPaciente,
                        @dtEraInicio = diagnostico.DtCreacion,
                        @iIdPorQuien = diagnostico.IIdUsuarioCreacion,//???
                        @iIdDonde = iIdSede,
                        @iIdUsuarioCreacion
                    };

                    var iIdCondicionEra = await dbCon.QueryFirstOrDefaultAsync<long>("sproc_JOB_CITA_RegistraCondicion", param, commandType: CommandType.StoredProcedure, transaction: transaction);

                    await RegistraCondicionAnotacion(iIdCondicionEra, diagnostico, dbCon, transaction);
                }
            }
        }

        /*
         9.- CONDICION ANOTACION
         */
        internal async Task RegistraCondicionAnotacion(long iIdCondicionEra, Diagnostico diagnostico, IDbConnection dbCon, IDbTransaction transaction)
        {
            var param = new
            {
                @iIdCondicionEra,
                @iIdPrueba = diagnostico.IIdPrueba,
                @vcIdCie10 = diagnostico.VcIdCie10,
                //@bRestriccion = diagnostico.BRestriccion,
                @dtControl = diagnostico.DtControl,
                @iIdUsuarioCreacion
            };

            var iIdCondicionAnotacion = await dbCon.QueryFirstOrDefaultAsync<long>("sproc_JOB_CITA_RegistraCondicionAnotacion", param, commandType: CommandType.StoredProcedure, transaction: transaction);

            await RegistraSeguimientoYFrecuencia(iIdCondicionAnotacion, diagnostico, dbCon, transaction);
        }

        /*
         10.- CONDICION_RESTRICCION_VIGILANCIA (seguimiento y frecuencia en días)
         */
        internal async Task RegistraSeguimientoYFrecuencia(long iIdCondicionAnotacion, Diagnostico diagnostico, IDbConnection dbCon, IDbTransaction transaction)
        {
            if (diagnostico.Seguimiento != null 
                //&& diagnostico.Seguimiento.FrecuenciaDias.HasValue 
                && diagnostico.Seguimiento.FrecuenciaDias > 0)
            {
                var param = new
                {
                    @iIdCondicionAnotacion,
                    @siFrecuenciaDias = diagnostico.Seguimiento.FrecuenciaDias,
                    @iIdUsuarioCreacion
                };

                await dbCon.QueryFirstOrDefaultAsync<long>("sproc_JOB_CITA_RegistraSeguimientoYFrecuencia", param, commandType: CommandType.StoredProcedure, transaction: transaction);
            }
        }

        /*
         Fin.- CITA_TRABAJADOR_DATA (actualiza flag migrado)
         */
        internal async Task FinalizaRigistroCitaData(int iIdCitaTrabajador, IDbConnection dbCon, IDbTransaction transaction)
        {
            var param = new
            {
                @iIdCitaTrabajador,
                @iIdUsuarioModificacion = iIdUsuarioCreacion
            };

            await dbCon.QueryFirstOrDefaultAsync<long>("sproc_JOB_CITA_FinalizaRigistro", param, commandType: CommandType.StoredProcedure, transaction: transaction);
        }
    }

    //internal class JsonData
    //{
    //    public JsonData()
    //    {
    //        Protocolos = new List<Protocolo>();
    //        Visitas = new List<Visita>();
    //    }

    //    public virtual IList<Protocolo> Protocolos { get; set; }
    //    public virtual IList<Visita> Visitas { get; set; }
    //}

    //internal class Protocolo
    //{
    //    public Protocolo()
    //    {
    //        Pruebas = new List<Prueba>();
    //    }
    //    public int IIdProtocolo { get; set; }
    //    public virtual IList<Prueba> Pruebas { get; set; }
    //}

    //internal class Visita
    //{
    //    public DateTime? DtInicio { get; set; }
    //    public DateTime? DtFin { get; set; }
    //    public int? IIdUsuarioInicio { get; set; }
    //    public int? IIdUsuarioFin { get; set; }
    //}

    //internal class Prueba
    //{
    //    public Prueba()
    //    {
    //        Fichas = new List<Ficha>();
    //        EntregablesGenerados = new List<Entregable>();
    //        Diagnosticos = new List<Diagnostico>();
    //    }
        
    //    public int IIdPrueba { get; set; }
    //    public string VcNombre { get; set; }
    //    public DateTime? DtInicioRegistroDatos { get; set; }
    //    public DateTime? DtFinRegistroDatos { get; set; }
    //    public int? IIdUsuarioInicioRegistroDatos { get; set; }
    //    public int? IIdUsuarioFinRegistroDatos { get; set; }
    //    public bool BCompletado { get; set; }
    //    public virtual IList<Ficha> Fichas { get; set; }
    //    public virtual IList<Entregable> EntregablesGenerados { get; set; }
    //    public virtual IList<Diagnostico> Diagnosticos { get; set; }
    //}

    //internal class Ficha
    //{
    //    public Ficha()
    //    {
    //        Campos = new List<Campo>();
    //    }
    //    public int IIdPrueba { get; set; }
    //    public int IIdFicha { get; set; }
    //    public string VcNombre { get; set; }
    //    public string VcDescripcion { get; set; }
    //    public virtual IList<Campo> Campos { get; set; }
    //}

    //internal class Campo
    //{
    //    public int IIdCampo { get; set; }
    //    public string VcNombre { get; set; }
    //    public short? ITipoValor { get; set; }
    //    //public string VcValor { get; set; }
    //    public object VcValor { get; set; }
    //}

    //internal class Diagnostico
    //{
    //    public int IIdPrueba { get; set; }
    //    public string VcIdCie10 { get; set; }
    //    public bool? BRestriccion { get; set; }
    //    public int? IIdUsuarioCreacion { get; set; }
    //    public DateTime? DtCreacion { get; set; }
    //    public DateTime? DtControl { get; set; }
    //    public DateTime? DtDesde { get; set; }
    //    public DateTime? DtHasta { get; set; }
    //    public Seguimiento Seguimiento { get; set; }
    //}

    //internal class Seguimiento
    //{
    //    public int? FrecuenciaDias { get; set; }
    //}

    //internal class Entregable
    //{
    //    public int IIdDocumento { get; set; }
    //}


}