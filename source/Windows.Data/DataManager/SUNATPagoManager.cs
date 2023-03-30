using ACME.Data.Contracts;
using ACME.Data.Entity;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;
using System.Data;
using System;
using Microsoft.Extensions.Logging;
using System.IO;
using System.Net;
using System.Collections.Generic;
using Nancy.Json;

namespace ACME.Data.DataManager
{
    public partial class SUNATPagoManager : DbFactoryBase, ISUNATPagoManager
    {
        private readonly IConfiguration _configuration;
        private readonly int iIdUsuario;
        private readonly ILogger<SUNATPagoManager> _logger;
        //private readonly IMensajePlantillaManager _msgManager;
        //private readonly string _sunat_provider;
        private readonly string _digiflow_url;
        private readonly string _tefacturo_url;
        private readonly string _tefacturo_apikey;
        private readonly string _tefacturo_ruc;
        private readonly float _igv;

        public SUNATPagoManager(IConfiguration configuration, ILogger<SUNATPagoManager> logger) : base(configuration, "SQLDBConnectionString", logger)
        {
            _configuration = configuration;
            iIdUsuario = int.Parse(@_configuration["WORKER_SUNATPago:IdUsuario"]);

            //_sunat_provider = _configuration["SUNAT_Provider"];
            _digiflow_url = _configuration["WORKER_SUNATPago:Digiflow:url"];
            _tefacturo_url = _configuration["WORKER_SUNATPago:TeFacturo:url"];
            _tefacturo_apikey = _configuration["WORKER_SUNATPago:TeFacturo:apikey"];
            _tefacturo_ruc = _configuration["WORKER_SUNATPago:TeFacturo:Emisor:numeroDocumentoIdentidad"];
            _igv = float.Parse(_configuration["WORKER_SAPAsientoServices:IGV"].ToString());
            _logger = logger;
        }

        #region sap
        //contado

        public async Task<ICollection<SAPAsientoContadoPen>> getSAPAsientosContadoPend()
        {
           
            return (ICollection<SAPAsientoContadoPen>)await DbQueryAsync<SAPAsientoContadoPen>("sproc_SAP_AsientoContado_Pendientes", null, true);
        }

        public async Task<SAPAsientoContadoCab> getSAPAsientosContadoCab(int iIdCitaSunatCab,string vcTipoDoc)
        {
            var param = new
            {
                @iIdCitaSunatCab,
                @vcTipoDoc
            };
            return await DbQuerySingleAsync<SAPAsientoContadoCab>("sproc_SAP_AsientoContado_CAB", param, true);
        }

        public async Task<ICollection<SAPAsientoContadoDet>> getSAPAsientosContadoDet(int iIdCitaSunatCab)
        {
            var param = new
            {
                @iIdCitaSunatCab,
                @_igv
            };
            return (ICollection<SAPAsientoContadoDet>)await DbQueryAsync<SAPAsientoContadoDet>("sproc_SAP_AsientoContado_DET", param, true);
        }

        public async Task<bool> updateSAPAsientosContado(int iIdCitaSunatCab,string DocEntry, string TransId)
        {
            var param = new
            {
                @iIdCitaSunatCab,
                @DocEntry,
                @TransId
            };
            return await DbExecuteAsync<bool>("sproc_SAP_AsientoContado_Update", param, true);
        }

        //NC Contado
        public async Task<ICollection<SAPAsientoContadoNCPen>> getSAPAsientosContadoPend_NC()
        {

            return (ICollection<SAPAsientoContadoNCPen>)await DbQueryAsync<SAPAsientoContadoNCPen>("sproc_SAP_AsientoContado_NC_Pendientes", null, true);
        }

        public async Task<SAPAsientoContadoCab> getSAPAsientosContadoCab_NC(int iIdCitaSunatCab,int? iIdCitaSunatDet, bool bCabecera)
        {
            var param = new
            {
                @iIdCitaSunatCab,
                @iIdCitaSunatDet,
                @bCabecera
            };
            return await DbQuerySingleAsync<SAPAsientoContadoCab>("sproc_SAP_AsientoContado_NC_CAB", param, true);
        }

        public async Task<ICollection<SAPAsientoContadoDet>> getSAPAsientosContadoDet_NC(int iIdCitaSunatCab, int? iIdCitaSunatDet, bool bCabecera)
        {
            var param = new
            {
                @iIdCitaSunatCab,
                @iIdCitaSunatDet,
                @bCabecera,
                @_igv
            };
            return (ICollection<SAPAsientoContadoDet>)await DbQueryAsync<SAPAsientoContadoDet>("sproc_SAP_AsientoContado_NC_DET", param, true);
        }

        public async Task<bool> updateSAPAsientosContado_NC(int iIdCitaSunatCab, int? iIdCitaSunatDet, bool bCabecera,string DocEntry,string TransId)
        {
            var param = new
            {
                iIdCitaSunatCab,
                @iIdCitaSunatDet,
                @bCabecera,
                @DocEntry,
                @TransId
            };
            return await DbExecuteAsync<bool>("sproc_SAP_AsientoContado_NC_Update", param, true);
        }


        //Crédito
        public async Task<ICollection<SAPAsientoContadoPen>> getSAPAsientosCreditoPend()
        {

            return (ICollection<SAPAsientoContadoPen>)await DbQueryAsync<SAPAsientoContadoPen>("sproc_SAP_AsientoCredito_Pendientes", null, true);
        }

        public async Task<SAPAsientoContadoCab> getSAPAsientosCreditoCab(long iIdValoCitaSunatCab)
        {
            var param = new
            {
                @iIdValoCitaSunatCab
            };
            return await DbQuerySingleAsync<SAPAsientoContadoCab>("sproc_SAP_AsientoCredito_CAB", param, true);
        }

        public async Task<ICollection<SAPAsientoContadoDet>> getSAPAsientosCreditoDet(long iIdValoCitaSunatCab)
        {
            var param = new
            {
                @iIdValoCitaSunatCab,
                @_igv
            };
            return (ICollection<SAPAsientoContadoDet>)await DbQueryAsync<SAPAsientoContadoDet>("sproc_SAP_AsientoCredito_DET", param, true);
        }

        public async Task<bool> updateSAPAsientosCredito(string DocEntry, string TransId)
        {
            var param = new
            {
                @DocEntry,
                @TransId
            };
            return await DbExecuteAsync<bool>("sproc_SAP_AsientoCredito_Update", param, true);
        }

        //ND Crédito
        public async Task<ICollection<SAPAsientoContadoPen>> getSAPAsientosCredito_ND_Pend()
        {

            return (ICollection<SAPAsientoContadoPen>)await DbQueryAsync<SAPAsientoContadoPen>("sproc_SAP_AsientoCredito_ND_Pendientes", null, true);
        }

        public async Task<SAPAsientoContadoCab> getSAPAsientosCredito_ND_Cab(long iIdValoCitaSunatCab,string DocEntry)
        {
            var param = new
            {
                @iIdValoCitaSunatCab,
                @DocEntry
            };
            return await DbQuerySingleAsync<SAPAsientoContadoCab>("sproc_SAP_AsientoCredito_ND_CAB", param, true);
        }

        public async Task<ICollection<SAPAsientoContadoDet>> getSAPAsientosCredito_ND_Det(long iIdValoCitaSunatCab, string DocEntry)
        {
            var param = new
            {
                @iIdValoCitaSunatCab,
                @DocEntry,
                @_igv
            };
            return (ICollection<SAPAsientoContadoDet>)await DbQueryAsync<SAPAsientoContadoDet>("sproc_SAP_AsientoCredito_ND_DET", param, true);
        }
        //NC Crédito
        public async Task<ICollection<SAPAsientoContadoPen>> getSAPAsientosCredito_NC_Pend()
        {

            return (ICollection<SAPAsientoContadoPen>)await DbQueryAsync<SAPAsientoContadoPen>("sproc_SAP_AsientoCredito_NC_Pendientes", null, true);
        }

        public async Task<SAPAsientoContadoCab> getSAPAsientosCredito_NC_Cab(long iIdValoCitaSunatCab, string DocEntry)
        {
            var param = new
            {
                @iIdValoCitaSunatCab,
                @DocEntry
            };
            return await DbQuerySingleAsync<SAPAsientoContadoCab>("sproc_SAP_AsientoCredito_NC_CAB", param, true);
        }

        public async Task<ICollection<SAPAsientoContadoDet>> getSAPAsientosCredito_NC_Det(long iIdValoCitaSunatCab, string DocEntry)
        {
            var param = new
            {
                @iIdValoCitaSunatCab,
                @DocEntry,
                @_igv
            };
            return (ICollection<SAPAsientoContadoDet>)await DbQueryAsync<SAPAsientoContadoDet>("sproc_SAP_AsientoCredito_NC_DET", param, true);
        }

        //Asiento Baja
        public async Task<ICollection<SAPAsientoContadoPen>> getSAPAsientosaja_Pend()
        {

            return (ICollection<SAPAsientoContadoPen>)await DbQueryAsync<SAPAsientoContadoPen>("sproc_SAP_AsientoBaja_Pendientes", null, true);
        }
        public async Task<bool> updateSAPAsientosBaja(string TransId)
        {
            var param = new
            {
               @TransId
            };
            return await DbExecuteAsync<bool>("sproc_SAP_AsientoCredito_Baja_Update", param, true);
        }
        #endregion sap


        public async Task<ICollection<EpisodiosEntregable>> getEpisodioxEntregablesPend(string vcCodEpisodioSync, int iIdusuario)
        {
            var param = new
            {
                @vcCodEpisodioSync,
                @iIdusuario
            };
            return (ICollection<EpisodiosEntregable>)await DbQueryAsync<EpisodiosEntregable>("sproc_job_episodio_generarpdf", param, true);
        }

        public async Task<ICollection<EpisodiosEnColaDocs>> getEpisodiosEnColaParaEntregables()
        {
            return (ICollection<EpisodiosEnColaDocs>)await DbQueryAsync<EpisodiosEnColaDocs>("sproc_job_episodios_cola_pdf", null, true);
        }

        public async Task<ICollection<PersonaTmp>> GetPersonasSinclaveTemporalAsync()
        {

            return (ICollection<PersonaTmp>)await DbQueryAsync<PersonaTmp>("sproc_generaclavemasiva_selectall", null, true);
        }

        public async Task<object> UpdateEpisodioEntregableRep(string vcCodEpisodioSync)
        {
            return await DbExecuteAsync<bool>("sproc_EPISODIO_UptEntregablesReprocesado", new { @vcCodEpisodioSync }, true);
        }


        public async Task<ICollection<NotaCreditoPendiente>> NotaCreditoPendDocs(int iIdUsuario)
        {
            var param = new
            {
                @iIdUsuario
            };

            return (ICollection<NotaCreditoPendiente>)await DbQueryAsync<NotaCreditoPendiente>("CITA_SUNAT_GetNotaCreditoPendienteDocs", param, true);
        }


        public async Task<IEnumerable<DocsxRecuperar>> DocsXGenerar(int iIdUsuario)
        {
            var param = new
            {
                @iIdUsuario
            };

            return await DbQueryAsync<DocsxRecuperar>("sproc_CITA_SUNAT_CABECERA_DocsXGenerar", param, true);
        }

        private async Task<object> ConsultarEstados(int iIdCitaSunatCab, int iIdUsuario)
        {
            var param = new
            {
                @iIdCitaSunatCab,
                @iIdUsuario
            };

            var citaSunat = await DbQuerySingleAsync<CitaSunatCabecera>("sproc_CITA_SUNAT_CABECERA_Select", param, true);

            object body = new
            {
                emisor = _tefacturo_ruc,
                numero = citaSunat.ISunatCorrelativo.ToString(),
                serie = citaSunat.VcSunatSerie,
                tipoComprobante = citaSunat.VcSunatTipoComprobante
            };

            var request = new TefacturoApi(this).ConsultarEstados(body);

            if (request.statusCode != HttpStatusCode.OK)
            {
                throw new Exception(request.resultContent);
            }

            return request.resultContent;
        }

        public async Task<DocFin> ConsultarPdfNotaCredito(NotaCreditoPendiente item, int iIdUsuario)
        {
            DocFin d = new DocFin();
            HttpStatusCode statusCode;
            string resultContent;
            byte[] sDecoded;
             object body = new
                {
                    tipoComprobante = item.tipoComprobante,
                    serie = item.Serie,
                    numero = item.Correlativo,
                    tipoArchivo = "pdf"
                };

                var str = item.tipoComprobante;
                str = str + "==> PDF " + item.Serie;
                str = str + " - " + item.Correlativo.ToString();

                _logger.LogInformation("SUNAT Nota de Crédito a recuperar al " + _digiflow_url + " para " + str);

                (statusCode, resultContent) = new DigiflowApi(this).ConsultarDocumento(body);
                //_logger.LogInformation(resultContent);

                if (statusCode != HttpStatusCode.OK)
                {
                    _logger.LogError("No se puede recupear el PDF se puede recuperar el PDF");
                    throw new Exception("No se puede recupear el PDF se puede recuperar el PDF");
                }

                dynamic data = new JavaScriptSerializer().DeserializeObject(resultContent);

                sDecoded = Convert.FromBase64String(data?.base64);

                var tipoComprobante = item.tipoComprobante;
                d.iIdCita = item.IIdCita;
                d.iIdCitaSunatCab = item.IIdCitaSunatCab;
                if (item.Tipo == "DET") d.iIdCitaSunatDet = item.Id;
                d.vcTipo = item.Tipo;
                d.vcExtension = ".pdf";
                d.vcNombre = tipoComprobante + " " + item.Serie + "-" + item.Correlativo.ToString();
                d.nvGUID = Guid.NewGuid().ToString();
                d.mem = new MemoryStream(sDecoded);

            return d;
        }

        public async Task<DocFin> ConsultarXmlNotaCredito(NotaCreditoPendiente item, int iIdUsuario)
        {
            DocFin d = new DocFin();
            HttpStatusCode statusCode;
            string resultContent;
            byte[] sDecoded;
            object body = new
            {
                tipoComprobante = item.tipoComprobante,
                serie = item.Serie,
                numero = item.Correlativo,
                tipoArchivo = "xml"
            };

            var str = item.tipoComprobante;
            str = str + "==> XML " + item.Serie;
            str = str + " - " + item.Correlativo.ToString();

            _logger.LogInformation("SUNAT Nota de Crédito a recuperar al " + _digiflow_url + " para " + str);

            (statusCode, resultContent) = new DigiflowApi(this).ConsultarDocumento(body);
            //_logger.LogInformation(/*resultContent*/);

            if (statusCode != HttpStatusCode.OK)
            {
                _logger.LogError("No se puede recupear el XML se puede recuperar el PDF");
                throw new Exception("No se puede recupear el XML se puede recuperar el PDF");
            }

            dynamic data = new JavaScriptSerializer().DeserializeObject(resultContent);

            sDecoded = Convert.FromBase64String(data?.base64);

            var tipoComprobante = item.tipoComprobante;
            d.iIdCita = item.IIdCita;
            d.iIdCitaSunatCab = item.IIdCitaSunatCab;
            if (item.Tipo == "DET") d.iIdCitaSunatDet = item.Id;
            d.vcTipo = item.Tipo;
            d.vcExtension = ".xml";
            d.vcNombre = tipoComprobante + " " + item.Serie + "-" + item.Correlativo.ToString();
            d.nvGUID = Guid.NewGuid().ToString();
            d.mem = new MemoryStream(sDecoded);

            return d;
        }

        public async Task<CitaSunatCabecera> ConsultarSunatCabecera(int iIdCitaSunatCab, int iIdUsuario)
        {
            object param = new
            {
                @iIdCitaSunatCab,
                @iIdUsuario
            };

            return await DbQuerySingleAsync<CitaSunatCabecera>("sproc_CITA_SUNAT_CABECERA_SelectJob", param, true);
        }



        public async Task<DocFin> ConsultarPdf(CitaSunatCabecera citaSunat, int iIdUsuario)
        {
            DocFin d = new DocFin();
            HttpStatusCode statusCode;
            string resultContent;
            byte[] sDecoded;
            
            object body = new
            {
                tipoComprobante = citaSunat.SiSunatTipo == 1 ? "F" : "B",
                serie = citaSunat.VcSunatSerie,
                numero = citaSunat.ISunatCorrelativo.ToString(),
                tipoArchivo = "pdf"
            };

            var str = citaSunat.SiSunatTipo == 1 ? "F" : "B";
            str = str + "==> PDF "+citaSunat.VcSunatSerie;
            str = str + " - " + citaSunat.ISunatCorrelativo.ToString();

            _logger.LogInformation("SUNAT Comprobante a recuperar al " + _digiflow_url + " para " + str);

            (statusCode, resultContent) = new DigiflowApi(this).ConsultarDocumento(body);

            if (statusCode != HttpStatusCode.OK)
            {
                _logger.LogError("No se puede recupear el PDF");
                throw new Exception("No se puede recupear el PDF");
            }

            dynamic data = new JavaScriptSerializer().DeserializeObject(resultContent);
            sDecoded = Convert.FromBase64String(data?.base64);
            var tipoComprobante = citaSunat.SiSunatTipo == 1 ? "FACTURA" : "BOLETA";
            d.iIdCita = citaSunat.IIdCita;
            d.iIdCitaSunatCab = citaSunat.IIdCitaSunatCab;
            d.vcExtension = ".pdf";
            d.vcNombre = tipoComprobante + " " + citaSunat.VcSunatSerie + "-" + citaSunat.ISunatCorrelativo.ToString();
            d.nvGUID = Guid.NewGuid().ToString();
            d.mem = new MemoryStream(sDecoded);
            

            return d;
        }

        public async Task<DocFin> ConsultarXml(CitaSunatCabecera citaSunat, int iIdUsuario)
        {
            DocFin d = new DocFin();
            HttpStatusCode statusCode;
            string resultContent;
            byte[] sDecoded;

            object body = new
            {
                tipoComprobante = citaSunat.SiSunatTipo == 1 ? "F" : "B",
                serie = citaSunat.VcSunatSerie,
                numero = citaSunat.ISunatCorrelativo.ToString(),
                tipoArchivo = "xml"
            };

            var str = citaSunat.SiSunatTipo == 1 ? "F" : "B";
            str = str + "==> XML " + citaSunat.VcSunatSerie;
            str = str + " - " + citaSunat.ISunatCorrelativo.ToString();

            _logger.LogInformation("SUNAT Comprobante a recuperar al " + _digiflow_url + " para " + str);

            (statusCode, resultContent) = new DigiflowApi(this).ConsultarDocumento(body);

            if (statusCode != HttpStatusCode.OK)
            {
                _logger.LogError("No se puede recupear el XML");
                throw new Exception("No se puede recupear el XML");
            }

            dynamic data = new JavaScriptSerializer().DeserializeObject(resultContent);
            sDecoded = Convert.FromBase64String(data?.base64);
            var tipoComprobante = citaSunat.SiSunatTipo == 1 ? "FACTURA" : "BOLETA";
            d.iIdCita = citaSunat.IIdCita;
            d.iIdCitaSunatCab = citaSunat.IIdCitaSunatCab;
            d.vcExtension = ".xml";
            d.vcNombre = tipoComprobante + " " + citaSunat.VcSunatSerie + "-" + citaSunat.ISunatCorrelativo.ToString();
            d.nvGUID = Guid.NewGuid().ToString();
            d.mem = new MemoryStream(sDecoded);


            return d;
        }

        public async Task<object> InsertDocument(DocFin doc)
        {
            try
            {
                var param = new
                {
                    @vcNombre = doc.vcNombre,
                    @iIdCita = doc.iIdCita,
                    @iIdCitaSunatCab=doc.iIdCitaSunatCab,
                    @vcRuta = doc.vcRuta,
                    @nvGUID = doc.nvGUID,
                    @vcExtension = doc.vcExtension,
                    @cTipo = doc.vcExtensionCorta,
                    @dtCreacion = DateTime.UtcNow,
                    @iIdUsuarioCreacion = 1,
                    @siTipoComprobante = doc.SiTipoComprobante
                };

                return await DbExecuteAsync<bool>("sproc_CITA_SUNAT_DOCS_Insert", param, true);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return Task.CompletedTask;
            }
        }

        public async Task<object> InsertDocumentNotaCredito(DocFin doc)//(int IIdCita, int iIdCitaSunatCab, string filepath, byte[] sDecoded, string tipo, string extension, int iIdUsuario)
        {
            try
            {
                var param = new
                {
                    @vcNombre = doc.vcNombre,
                    @iIdCita = doc.iIdCita,
                    @iIdCitaSunatCab = doc.iIdCitaSunatCab,
                    @iIdCitaSunatDet = doc.iIdCitaSunatDet,
                    @vcRuta = doc.vcRuta,
                    @nvGUID = doc.nvGUID,
                    @vcExtension = doc.vcExtension,
                    @cTipo = doc.vcTipo,
                    @dtCreacion = DateTime.UtcNow,
                    @iIdUsuarioCreacion = 1,
                    @siTipoComprobante = doc.SiTipoComprobante
                };

                return await DbExecuteAsync<bool>("sproc_CITA_SUNAT_DOCS_InsertNC", param, true);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return Task.CompletedTask;
            }
        }



        
    }
}