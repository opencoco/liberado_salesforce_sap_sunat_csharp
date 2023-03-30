using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using ACME.Data.Contracts;
using ACME.Data.Entity;
using ACME.WorkerService.Helpers;
using System;
using System.IO;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace ACME.WorkerService.Tasks
{
    internal class SUNATPagoTasks : ISUNATPagoTasks
    {

        private readonly ISUNATPagoManager _sunat;
        private readonly IConfiguration _configuration;
        private readonly ILogger _logger;
        private readonly ICloudStorage _cloudStorage;
        private readonly IPDFService _pdfservice;

        public SUNATPagoTasks(IPDFService pdfservice,ISUNATPagoManager sunat, ILogger<SUNATPagoTasks> logger, IConfiguration configuration, ICloudStorage cloudStorage)
        {
            _pdfservice = pdfservice;
            _sunat = sunat;
            _configuration = configuration;
            _logger = logger;
            _cloudStorage = cloudStorage;
            InitTasks();
        }

        private void InitTasks()
        {
            string repo1 = _configuration["WORKER_SUNATPago:TeFacturo:repoDocs"];
            string repo2 = _configuration["WORKER_SUNATPago:Digiflow:repoDocs"];
            //string repo3 = _configuration["WORKER_SUNATPago:TeFacturo:repoDocs"];

            if (!Directory.Exists(repo1))
            {
                Directory.CreateDirectory(repo1);
            }

            if (!Directory.Exists(repo2))
            {
                Directory.CreateDirectory(repo2);
            }
        }

        #region nota_credito

        public async Task<object> RecuperarNotasCredito()
        {
            _logger.LogInformation("-----> Recuperar notas de crédito inicio");

            var lista = await _sunat.NotaCreditoPendDocs(1);
            foreach (NotaCreditoPendiente item in lista)
            {
                try
                {
                    //****************************PDF***************************************
                    if (!item.BOkGeneracionNCPdf) { 
                        var doc = await _sunat.ConsultarPdfNotaCredito(item, 1);
                        if (doc != null && !string.IsNullOrEmpty(doc.nvGUID))
                        {
                            DateTime d = DateTime.Now;
                            string dateString = d.ToString("yyyyMMddHHmmss");
                            doc.vcNombre = doc.vcNombre + doc.vcExtension;

                            var filenamecloud = @"SUNAT/" + doc.nvGUID + "_" + doc.vcNombre;
                            var pathcloud = await _cloudStorage.UploadFileAsync(doc.mem, filenamecloud);
                            doc.vcRuta = pathcloud;
                            doc.SiTipoComprobante = 7;

                            await _sunat.InsertDocumentNotaCredito(doc);
                        }
                    }
                    //****************************XML***************************************
                    if (!item.BOkGeneracionNCXml)
                    {
                        var doc = await _sunat.ConsultarXmlNotaCredito(item, 1);
                        if (doc != null && !string.IsNullOrEmpty(doc.nvGUID))
                        {
                            DateTime d = DateTime.Now;
                            string dateString = d.ToString("yyyyMMddHHmmss");
                            doc.vcNombre = doc.vcNombre + doc.vcExtension;

                            var filenamecloud = @"SUNAT/" + doc.nvGUID + "_" + doc.vcNombre;
                            var pathcloud = await _cloudStorage.UploadFileAsync(doc.mem, filenamecloud);
                            doc.vcRuta = pathcloud;
                            doc.SiTipoComprobante = 7;
                            await _sunat.InsertDocumentNotaCredito(doc);
                        }
                    }
                }
                catch (Exception ex) { _logger.LogError(ex.StackTrace.ToString()); }
            }

            _logger.LogInformation("-----> Recuperar notas de crédito fin");

            return true;
        }


        #endregion nota_credito

        #region Comprobantes

        public async Task<object> RegistrarComprobantes()
        {
            _logger.LogInformation("-----> Recuperar Comprobantes inicio");

            var lista=await _sunat.DocsXGenerar(1);
            foreach (DocsxRecuperar item in lista)
            {
                try
                {
                    var citaSunat = await _sunat.ConsultarSunatCabecera(item.IIdCitaSunatCab, 1);
                    //*****************************PDF**********************
                    if (!item.BOkGeneracionComprobantePdf) {
                        var doc = await _sunat.ConsultarPdf(citaSunat, 1);
                        if (doc != null && !string.IsNullOrEmpty(doc.nvGUID))
                        {
                            DateTime d = DateTime.Now;
                            string dateString = d.ToString("yyyyMMddHHmmss");
                            doc.vcNombre = doc.vcNombre + doc.vcExtension;
                        
                            var filenamecloud = @"SUNAT/" + doc.nvGUID + "_" + doc.vcNombre;
                            var pathcloud = await _cloudStorage.UploadFileAsync(doc.mem, filenamecloud);
                            doc.vcRuta = pathcloud;
                            doc.vcExtensionCorta = "P";
                            doc.SiTipoComprobante = short.Parse(citaSunat.VcSunatTipoComprobante);
                            await _sunat.InsertDocument(doc);
                        }
                    }
                    //***************************XML***********************
                    if (!item.BOkGeneracionComprobanteXml)
                    {
                        var doc = await _sunat.ConsultarXml(citaSunat, 1);
                        if (doc != null && !string.IsNullOrEmpty(doc.nvGUID))
                        {
                            DateTime d = DateTime.Now;
                            string dateString = d.ToString("yyyyMMddHHmmss");
                            doc.vcNombre = doc.vcNombre + doc.vcExtension;

                            var filenamecloud = @"SUNAT/" + doc.nvGUID + "_" + doc.vcNombre;
                            var pathcloud = await _cloudStorage.UploadFileAsync(doc.mem, filenamecloud);
                            doc.vcRuta = pathcloud;
                            doc.vcExtensionCorta = "X";
                            doc.SiTipoComprobante = short.Parse(citaSunat.VcSunatTipoComprobante);
                            await _sunat.InsertDocument(doc);
                        }
                    }

                }
                catch(Exception ex) { _logger.LogError(ex.StackTrace.ToString()); }
            }

            _logger.LogInformation("-----> Recuperar Comprobantes fin");

            return true;
        }


        #endregion Comprobantes

        #region entregables
        /*
        public async Task<object> GenerarEntregablesPendientes()
        {
            try
            {
                _logger.LogInformation("-----> Generar entregables pendientes ini");
                var pendientes = await _sunat.getEpisodiosEnColaParaEntregables();
                foreach (var episodio in pendientes)
                {
                    _logger.LogInformation("-----> procesando entregables del episodio = " + episodio.VcCodEpisodioSync);
                    var entregablesencola = await _sunat.getEpisodioxEntregablesPend(episodio.VcCodEpisodioSync, 1);
                    foreach (var entregable in entregablesencola)
                    {
                        try
                        {
                            _logger.LogInformation("-----> procesando entregable = "+ entregable.VcCodigoCompatible);
                            await GenerateEntregablePdf((int)entregable.IIdCitaTrabaTitular, (int)entregable.IIdMensajePlantilla, (int)entregable.IIdSubClaseDocumento, episodio.VcCodEpisodioSync, (bool)entregable.BIncluirFirma);
                        }
                        catch (Exception ex) { _logger.LogError(ex.Message); }
                    }
                }
                _logger.LogInformation("-----> Generar entregables pendientes fin");

                return true;
            }
            catch (Exception ex) { _logger.LogError(ex.Message);
                return false;
            }
        }

        private async Task<object> GenerateEntregablePdf(int iIdCitaTrabaTitular, int iIdMensajePlantilla, int iIdSubClaseDocumento, string vcCodEpisodioSync,bool bIncluirFirma)
        {
            object data;

            var cita = await _sunat.getDetalleCitaProtocolo(iIdCitaTrabaTitular);
            if (cita != null)
            {
                var cita_data = await _sunat.getDataEpisodioxEntregable(iIdSubClaseDocumento, vcCodEpisodioSync);
                if (cita_data == null)
                {
                    throw new Exception($"No tiene data de evaluacion {iIdCitaTrabaTitular}");
                }

                string firma = "";

                if (bIncluirFirma)
                {
                    var especialistas = await _sunat.getFirmaEpisodioxEntregable(iIdSubClaseDocumento, vcCodEpisodioSync);
                    if (especialistas == null)
                    {
                        throw new Exception($"El especialista no tiene la firma");
                    }

                    foreach (var item in especialistas)
                    {
                        if (item.VbDocumento != null)
                        {
                            MemoryStream stream = new MemoryStream(item.VbDocumento) { Position = 0 };
                            firma = Convert.ToBase64String(stream.ToArray());
                        }
                    }

                }

                var plantilla = await _sunat.getPlantillaEpisodioxEntregable(iIdMensajePlantilla);
                if (plantilla == null || string.IsNullOrEmpty(plantilla.VcMensaje))
                {
                    throw new Exception($"No existe la plantilla {iIdMensajePlantilla}");
                }

                if (string.IsNullOrEmpty(plantilla.JsonParam))
                {
                    throw new Exception($"No tiene JsonParam la plantilla {iIdMensajePlantilla}");
                }

                
               

                string nombreDocumento = $"{vcCodEpisodioSync}_{plantilla.VcNombre.Trim()}";
                //string ruta = $"{vcCodEpisodioSync}_{plantilla.VcNombre.Trim()}.pdf";
                string htmlContent = plantilla.VcMensaje;

                //var options = new JsonSerializerOptions
                //{
                //    PropertyNameCaseInsensitive = true,
                //};
                //var jsonParam = JsonSerializer.Deserialize<List<Variable>>(plantilla.JsonParam, options);--old
                //var jsonParam = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Variable>>(plantilla.JsonParam);
                //dynamic jsonObject = Newtonsoft.Json.JsonConvert.DeserializeObject(cita_data.JsonData);
                //var protocolos = jsonObject["protocolo"];
                var variables = new Dictionary<string, string>();
                //foreach (var variable in jsonParam)
                //{
                //    var valorVariable = GetValorVariable(protocolos, variable.path.idprotocolo, variable.path.idprueba, variable.path.idficha, variable.path.idcampo, variable.valores);
                //    variables.Add(variable.nombre, valorVariable);
                //}
                foreach (var variable in cita_data)
                {
                    //var valorVariable = GetValorVariable(protocolos, variable.path.idprotocolo, variable.path.idprueba, variable.path.idficha, variable.path.idcampo, variable.valores);
                    string valorVariable = variable.VcValor;
                    if ((bool)variable.BEsDecimal) valorVariable = variable.DeValor.ToString();
                    variables.Add(variable.VcCodigoFicha+'.'+variable.VcCodigoDato, valorVariable);
                }

                foreach (KeyValuePair<string, string> item in variables)
                {
                    htmlContent = htmlContent.Replace("{" + item.Key + "}", item.Value);
                }

                string guid = Guid.NewGuid().ToString();
                string link = @_configuration["webroot"] + "validadocumento?guid=" + guid;
                string qrcode = _pdfservice.Base64QR(link);
                string fecha_prueba = cita.DtCreacion.ToShortDateString();
                string masculino = "0";
                string femenino = "0";

                if (cita.SiTipoIdentificacion == 1) masculino = "1";
                if (cita.SiTipoIdentificacion == 2) femenino = "1";

                Dictionary<string, string> datos = new Dictionary<string, string>
                {
                    { "nombres_apellidos", ($"{cita.VcApellidoPaterno} {cita.VcApellidoMaterno}, {cita.VcNombres}").ToUpper() },
                    { "dni", cita.VcNumeroIdentificacion },
                    { "empresa", cita.VcEmpresaEmpleador?.ToUpper() },
                    { "qrcode", qrcode },
                    { "fecha_evaluacion", fecha_prueba },
                    { "link", link},
                    { "fecha_nacimiento", cita.DNacimiento?.ToString("dd/MMyyyy")},
                    { "masculino", masculino},
                    { "femenino", femenino},
                    { "tipo_documento", cita.VcTipoDocumento},
                    { "telefono", cita.VcCelular},
                    { "hcl", cita.VcHCD },
                    { "edad", cita.Edad.ToString()}
                };

                if (bIncluirFirma)
                {
                    datos.Add("firma", firma);
                }

                    foreach (KeyValuePair<string, string> item in datos)
                {
                    htmlContent = htmlContent.Replace("{" + item.Key + "}", item.Value);
                }

                byte[] outPdfBuffer = _pdfservice.GenerarPdfStream(htmlContent);

                var dFecha = DateTime.Now.ToString("MMyyyy");

                var filenamecloud = @""+ dFecha + "/" + guid + "_" + vcCodEpisodioSync + ".pdf";
                var pathcloud = await _cloudStorage.UploadFileAsync(new MemoryStream(outPdfBuffer), filenamecloud);

                DocFin doc = new DocFin();
                doc.vcNombre = nombreDocumento;
                doc.vcRuta = pathcloud;
                doc.iIdSubClaseDocumento = iIdSubClaseDocumento;
                doc.idUsuario = 1;
                doc.nvGUID = guid;
                doc.vcExtension = ".pdf";
                var iddoc = await _sunat.InsertaDocumento(doc);
                await _sunat.InsertaEntregableaEpisodio(doc, iddoc, vcCodEpisodioSync);

                //var param = new
                //{
                //    @vcNombre = nombreDocumento,
                //    @vcRuta = pathcloud,
                //    @siTipoDoc = 20,//int.Parse(@_configuration["AppSettings:GeneratePdf:SiTipoDoc"]),
                //    @vbDocumento = outPdfBuffer,
                //    @iIdSubClaseDocumento = iIdSubClaseDocumento,//int.Parse(@_configuration["AppSettings:GeneratePdf:IdSubClaseDocumento"]),
                //    @nvGUID = guid,
                //    @vcExtension = ".pdf",
                //    @siEstado = 1,
                //    @iIdUsuarioCreacion = iIdUsuarioCreacion,
                //    @iIdCitaTrabajador = iIdCitaTrabajador,
                //    @iIdPrueba = iIdPrueba
                //};

                //var iIdDocumento = await DbQuerySingleAsync<long>("sproc_DOCUMENTOInsertByExtramuro", param, true);

                data = new
                {
                    iddoc,
                    guid
                };
            }
            else
            {
                throw new Exception($"No existe la cita trabajador {iIdCitaTrabaTitular}");
            }

            return data;
        }

        string GetValorVariable(dynamic protocolo, int IdProtocolo, int IdPrueba, int IdFicha, int IdCampo, IList<string> valores)
        {
            string valorVariable = "";

            //foreach (var protocolo in protocolos)
            //{
            //if (protocolo["iIdProtocolo"] == IdProtocolo)
            //{
            var pruebas = protocolo["pruebas"];
            foreach (var prueba in pruebas)
            {
                if (prueba["iIdPrueba"] == IdPrueba)
                {
                    var fichas = prueba["fichas"];
                    foreach (var ficha in fichas)
                    {
                        if (ficha["iIdFicha"] == IdFicha)
                        {
                            var campos = ficha["campos"];
                            foreach (var campo in campos)
                            {
                                if (campo["iIdCampo"] == IdCampo)
                                {
                                    if (campo.ContainsKey("valor"))
                                    {
                                        string valor = campo["valor"];
                                        if (valores.Contains(valor))
                                        {
                                            valorVariable = "X";
                                        }
                                    }
                                    break;
                                }
                            }
                            break;
                        }
                    }
                    break;
                }
            }
            //break;
            //}
            //}

            return valorVariable;
        }
        */
        #endregion entregables


    }
}
