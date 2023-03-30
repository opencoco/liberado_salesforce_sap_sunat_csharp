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
    public class MiHistoriaManager : DbFactoryBase, IMiHistoriaManager
    {
        private readonly IConfiguration _configuration;
        private readonly int iIdUsuarioCreacion;
        private readonly ILogger<MiHistoriaManager> _logger;

        public MiHistoriaManager(IConfiguration configuration, ILogger<MiHistoriaManager> logger) : base(configuration, "SQLDBConnectionString", logger)
        {
            _configuration = configuration;
            //iIdUsuarioCreacion = int.Parse(@_configuration["SUNATServices:IdUsuario"]);
            _logger = logger;
        }

        public async Task<object> RegistraMiHistoriaTipo1()
        {
            try
            {
                var result = await DbExecuteAsync<object>("sproc_JOB_MI_HISTORIA_RegistraTipo1", null, true);

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex.StackTrace);
                throw;
            }
        }

        public async Task<object> RegistraMiHistoriaTipo2()
        {
            try
            {
                var citas = await DbQueryAsync<CitaTrabajadorData>("sproc_JOB_MI_HISTORIA_GetData", null, true);

                var dt = new DataTable();
                dt.Columns.Add("iIdCitaTrabData", typeof(int));
                dt.Columns.Add("iIdUsuario", typeof(int));
                dt.Columns.Add("dtFechaHoraHistoria", typeof(DateTime));
                dt.Columns.Add("vcReferenceKey", typeof(string));
                dt.Columns.Add("vcGuid", typeof(string));
                dt.Columns.Add("iIdSubClaseDocumento", typeof(int));
                dt.Columns.Add("vcConcepto", typeof(string));

                foreach (var cita in citas)
                {
                    try
                    {
                        var jsonData = Newtonsoft.Json.JsonConvert.DeserializeObject<JsonData>(cita.JsonData);
                        var pruebas = new List<Prueba>();

                        foreach (var protocolo in jsonData.Protocolos)
                        {
                            foreach (var prueba in protocolo.Pruebas)
                            {
                                foreach (var x in prueba.EntregablesGenerados)
                                {
                                    dt.Rows.Add(cita.IIdCitaTrabData, cita.IIdUsuario, cita.DtModificacion, x.IIdDocumento, x.Guid, x.IIdSubClaseDocumento, x.VcReporteQueGenera);
                                }
                            }
                        }
                        //await RegistraData(jsonData, cita.IIdCitaTrabajador, cita.IIdPaciente, cita.IIdSede);
                    }
                    catch (Exception ex)
                    {
                        _logger.Log(LogLevel.Error, ex, "Error al intentar ejecutar RegistraMiHistoriaTipo2.");
                        throw;
                    }

                }

                var param = new
                {
                    @tblMiHistoria = dt
                };

                var result = await DbExecuteAsync<object>("sproc_JOB_MI_HISTORIA_RegistraTipo2", param, true);

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex.StackTrace);
                throw;
            }
        }

        public async Task<object> RegistraMiHistoriaTipo3()
        {
            try
            {
                var result = await DbExecuteAsync<object>("sproc_JOB_MI_HISTORIA_RegistraTipo3", null, true);

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex.StackTrace);
                throw;
            }
        }


    }
}