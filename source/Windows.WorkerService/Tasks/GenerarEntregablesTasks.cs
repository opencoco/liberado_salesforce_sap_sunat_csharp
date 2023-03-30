using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using ACME.Data.Contracts;
using ACME.Data.Entity;
using ACME.WorkerService.Helpers;
using System;
using System.IO;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Text.Json;
using System.Linq;
using System.Net;
using ACME.WorkerService.Tools;
using Hanssens.Net;

namespace ACME.WorkerService.Tasks
{
    internal interface IGenerarEntregablesTasks
    {
        Task<object> GenerarEntregablesPendientes();
    }

    internal class GenerarEntregablesTasks : IGenerarEntregablesTasks
    {
        private readonly ISUNATPagoManager _sunat;
        private readonly IConfiguration _configuration;
        private readonly ILogger _logger;
        private readonly ICloudStorage _cloudStorage;
        private readonly IPDFService _pdfservice;
        private readonly string _logoPath;
        private readonly string _urlApiDocumento;
        private readonly int iIdusuario;

        public GenerarEntregablesTasks(IPDFService pdfservice, ISUNATPagoManager sunat, ILogger<GenerarEntregablesTasks> logger, IConfiguration configuration, ICloudStorage cloudStorage)
        {
            _pdfservice = pdfservice;
            _sunat = sunat;
            _configuration = configuration;
            _logger = logger;
            _cloudStorage = cloudStorage;
            _logoPath = @_configuration["LogoPath"];
            _urlApiDocumento = @_configuration["WORKER_GenerarEntregables:UrlApiOnline"];
            iIdusuario = int.Parse(_configuration["WORKER_GenerarEntregables:IdUsuario"]);
            InitTasks();
        }

        private void InitTasks()
        {
            

        }

        //public async Task<object> GenerarEntregablesPendientes()
        //{
        //    try
        //    {
        //        _logger.LogInformation("-----> Generar entregables pendientes ini");
        //        var pendientes = await _sunat.getEpisodiosEnColaParaEntregables();
        //        var storage = new LocalStorage();
        //        var accessToken = "";
        //        string key = "bearerkey";
        //        try
        //        {
        //            accessToken = storage.Get<string>(key);
        //        }
        //        catch (Exception ex)
        //        {
        //            storage.Store(key, "");
        //        }



        //        if (string.IsNullOrEmpty(accessToken))
        //        {
        //            object body = new
        //            {
        //                email = @_configuration["WORKER_GenerarEntregables:user"],
        //                password = @_configuration["WORKER_GenerarEntregables:pwd"]
        //            };

        //            var _request = GetToken(body);
        //            if (_request.statusCode != HttpStatusCode.OK)
        //            {
        //                object request = new
        //                {
        //                    _request.statusCode,
        //                    _request.resultContent
        //                };

        //                _logger.LogError(Newtonsoft.Json.JsonConvert.SerializeObject(request));
        //            }
        //            else
        //            {
        //                var _jsonreschaman = Newtonsoft.Json.JsonConvert.DeserializeObject<ResponseBody>(_request.resultContent);
        //                storage.Store(key, _jsonreschaman.Result.Token);
        //                accessToken = _jsonreschaman.Result.Token;
        //            }
        //        }



        //        foreach (var episodio in pendientes)
        //        {
        //            _logger.LogInformation("-----> procesando entregables del episodio = " + episodio.VcCodEpisodioSync);
        //            try
        //            {
        //                object bodygenerate = new
        //                {
        //                    codepisodio = episodio.VcCodEpisodioSync,
        //                    codaptitud = episodio.VcAptitud,
        //                };

        //                var _requestgenerate = ApiGenerateDocument(bodygenerate, accessToken);
        //                if (_requestgenerate.statusCode != HttpStatusCode.Unauthorized)
        //                {
        //                    object body = new
        //                    {
        //                        email = @_configuration["WORKER_GenerarEntregables:user"],
        //                        password = @_configuration["WORKER_GenerarEntregables:pwd"]
        //                    };

        //                    var _request = GetToken(body);
        //                    if (_request.statusCode != HttpStatusCode.OK)
        //                    {
        //                        object request = new
        //                        {
        //                            _request.statusCode,
        //                            _request.resultContent
        //                        };

        //                        _logger.LogError(Newtonsoft.Json.JsonConvert.SerializeObject(request));
        //                    }
        //                    else
        //                    {
        //                        var _jsonreschaman = Newtonsoft.Json.JsonConvert.DeserializeObject<ResponseBody>(_request.resultContent);
        //                        storage.Store(key, _jsonreschaman.Result.Token);
        //                        accessToken = _jsonreschaman.Result.Token;
        //                    }

        //                    _requestgenerate = ApiGenerateDocument(bodygenerate, accessToken);
        //                }

        //                if (_requestgenerate.statusCode != HttpStatusCode.OK)
        //                {
        //                    object requestge = new
        //                    {
        //                        episodio.VcCodEpisodioSync,
        //                        _requestgenerate.statusCode,
        //                        _requestgenerate.resultContent
        //                    };

        //                    _logger.LogError(Newtonsoft.Json.JsonConvert.SerializeObject(requestge));
        //                }
        //                else
        //                {
        //                    var _jsonreschaman = Newtonsoft.Json.JsonConvert.DeserializeObject<ResponseResult>(_requestgenerate.resultContent);
        //                    if (!_jsonreschaman.Result) _logger.LogError("Generacion de entregables con error del episodio: " + episodio.VcCodEpisodioSync);
        //                }

        //                await _sunat.UpdateEpisodioEntregableRep(episodio.VcCodEpisodioSync);
        //            }
        //            catch (Exception ex)
        //            {
        //                _logger.LogError("Error procesando el apisodio: " + episodio.VcCodEpisodioSync + ", " + ex.Message);
        //            }
        //        }

        //        _logger.LogInformation("-----> Generar entregables pendientes fin");

        //        return true;
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError(ex.Message);
        //        return false;
        //    }
        //}

        //cambio tmp por
        public async Task<object> GenerarEntregablesPendientes()
        {
            try
            {
                _logger.LogInformation("-----> Generar cambio de clave chinalco");
                //var pendientes = await _sunat.getEpisodiosEnColaParaEntregables();
                var personas = await _sunat.GetPersonasSinclaveTemporalAsync();
                var storage = new LocalStorage();
                var accessToken = "";
                string key = "bearerkey";
                try
                {
                    accessToken = storage.Get<string>(key);
                }
                catch (Exception ex)
                {
                    storage.Store(key, "");
                }



                if (string.IsNullOrEmpty(accessToken))
                {
                    object body = new
                    {
                        email = @_configuration["WORKER_GenerarEntregables:user"],
                        password = @_configuration["WORKER_GenerarEntregables:pwd"]
                    };

                    var _request = GetToken(body);
                    if (_request.statusCode != HttpStatusCode.OK)
                    {
                        object request = new
                        {
                            _request.statusCode,
                            _request.resultContent
                        };

                        _logger.LogError(Newtonsoft.Json.JsonConvert.SerializeObject(request));
                    }
                    else
                    {
                        var _jsonreschaman = Newtonsoft.Json.JsonConvert.DeserializeObject<ResponseBody>(_request.resultContent);
                        storage.Store(key, _jsonreschaman.Result.Token);
                        accessToken = _jsonreschaman.Result.Token;
                    }
                }



                foreach (PersonaTmp person in personas)
                {
                    _logger.LogInformation("-----> cambiando clave de = " + person.VcNumeroIdentificacion);
                    try
                    {
                        //object bodygenerate = new
                        //{
                        //    codepisodio = episodio.VcCodEpisodioSync,
                        //    codaptitud = episodio.VcAptitud,
                        //};

                        var _requestgenerate = ApiGenerateCambioclave(accessToken);
                        if (_requestgenerate.statusCode != HttpStatusCode.Unauthorized)
                        {
                            object body = new
                            {
                                email = @_configuration["WORKER_GenerarEntregables:user"],
                                password = @_configuration["WORKER_GenerarEntregables:pwd"]
                            };

                            var _request = GetToken(body);
                            if (_request.statusCode != HttpStatusCode.OK)
                            {
                                object request = new
                                {
                                    _request.statusCode,
                                    _request.resultContent
                                };

                                _logger.LogError(Newtonsoft.Json.JsonConvert.SerializeObject(request));
                            }
                            else
                            {
                                var _jsonreschaman = Newtonsoft.Json.JsonConvert.DeserializeObject<ResponseBody>(_request.resultContent);
                                storage.Store(key, _jsonreschaman.Result.Token);
                                accessToken = _jsonreschaman.Result.Token;
                            }

                            _requestgenerate = ApiGenerateCambioclave(accessToken);
                        }

                        if (_requestgenerate.statusCode != HttpStatusCode.OK)
                        {
                            object requestge = new
                            {
                                person.VcNumeroIdentificacion,
                                _requestgenerate.statusCode,
                                _requestgenerate.resultContent
                            };

                            _logger.LogError(Newtonsoft.Json.JsonConvert.SerializeObject(requestge));
                        }
                        else
                        {
                            var _jsonreschaman = Newtonsoft.Json.JsonConvert.DeserializeObject<ResponseResult>(_requestgenerate.resultContent);
                            if (!_jsonreschaman.Result) _logger.LogError("Error cambiando clave: " + person.VcNumeroIdentificacion);
                        }

                        //await _sunat.UpdateEpisodioEntregableRep(episodio.VcCodEpisodioSync);
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError("Error cambiando clave: " + person.VcNumeroIdentificacion + ", " + ex.Message);
                    }
                }

                _logger.LogInformation("-----> Generar cambio de clave chinalco fin");

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return false;
            }
        }

        private (HttpStatusCode statusCode, string resultContent) ApiGenerateDocument(dynamic body,string key)
            {
                return Helper.Request(body, _urlApiDocumento+ "/api/v1/integracion/episodio-entregables-generar-x-online", key, "post");
            }

        private (HttpStatusCode statusCode, string resultContent) ApiGenerateCambioclave( string key)
        {
            return Helper.Request(null, _urlApiDocumento + "/api/v1/cuenta/cambiar-claves-masivo", key, "post");
        }

        private (HttpStatusCode statusCode, string resultContent)  GetToken(dynamic body)
            {
                return Helper.Request(body, _urlApiDocumento+ "/api/v1/cuenta/login-telemetry", null, "post");
            }

        internal class ResponseBody
        {
           
            public string Message { get; set; }
            public Result Result { get; set; }
        }
        internal class Result
        {

            public string Token { get; set; }
        }
        internal class ResponseResult
        {

            public string Message { get; set; }
            public bool Result { get; set; }
        }

        //    private async Task<object> GenerateEntregablePdf(int iIdCitaTrabaTitular, int iIdMensajePlantilla, int iIdSubClaseDocumento, string vcCodEpisodioSync, bool bIncluirFirma, string clase)
        //    {
        //        object data;

        //        var cita = await _sunat.getDetalleCitaProtocolo(iIdCitaTrabaTitular, iIdSubClaseDocumento);
        //        if (cita != null)
        //        {
        //            var cita_data = await _sunat.getDataEpisodioxEntregable(iIdSubClaseDocumento, vcCodEpisodioSync);
        //            if (cita_data == null)
        //            {
        //                throw new Exception($"No tiene data de evaluacion {iIdCitaTrabaTitular}");
        //            }

        //            string firma = "";

        //            if (bIncluirFirma)
        //            {
        //                var especialistas = await _sunat.getFirmaEpisodioxEntregable(iIdSubClaseDocumento, vcCodEpisodioSync);
        //                if (especialistas == null)
        //                {
        //                    throw new Exception($"El especialista no tiene la firma");
        //                }

        //                foreach (var item in especialistas)
        //                {
        //                    if (item.VbDocumento != null)
        //                    {
        //                        MemoryStream stream = new(item.VbDocumento) { Position = 0 };
        //                        firma = Convert.ToBase64String(stream.ToArray());
        //                    }
        //                }
        //            }

        //            var plantilla = await _sunat.getPlantillaEpisodioxEntregable(iIdMensajePlantilla);
        //            if (plantilla == null || string.IsNullOrEmpty(plantilla.VcMensaje))
        //            {
        //                throw new Exception($"No existe la plantilla {iIdMensajePlantilla}");
        //            }

        //            if (string.IsNullOrEmpty(plantilla.JsonParam))
        //            {
        //                throw new Exception($"No tiene JsonParam la plantilla {iIdMensajePlantilla}");
        //            }

        //            string nombreDocumento = $"{vcCodEpisodioSync}_{plantilla.VcNombre.Trim()}";
        //            //string ruta = $"{vcCodEpisodioSync}_{plantilla.VcNombre.Trim()}.pdf";
        //            string htmlContent = plantilla.VcMensaje;
        //            string guid = Guid.NewGuid().ToString();
        //            string link = @_configuration["webroot"] + "validadocumento?guid=" + guid;
        //            string qrcode = _pdfservice.Base64QR(link);
        //            //string fecha_prueba = cita.DtCreacion.ToShortDateString();
        //            //string masculino = "0";
        //            //string femenino = "0";

        //            //if (cita.SiTipoIdentificacion == 1) masculino = "1";
        //            //if (cita.SiTipoIdentificacion == 2) femenino = "1";

        //            /************************ header ************************/
        //            var jsonParam = JsonSerializer.Deserialize<Variable>(plantilla.JsonParam, new JsonSerializerOptions { PropertyNameCaseInsensitive = true, });

        //            string logo = await GetLogoBase64(jsonParam.datos_documento?.logopersonalizado == "si", iIdCitaTrabaTitular);

        //            Dictionary<string, string> datos = new()
        //            {
        //                { "logo", logo },
        //                { "qrcode", qrcode },
        //                { "link", link },

        //                { "fecha_inicial_examen", cita.Fecha_inicial_examen },
        //                { "motivo_evaluacion", cita.Motivo_evaluacion },
        //                { "tipo_emo", cita.Tipo_emo },
        //                { "clinica_evaluadora", cita.Clinica_evaluadora },
        //                { "direccion_clinica", cita.Direccion_clinica },
        //                { "tiempo_laborando", cita.Tiempo_laborando?.ToString() },
        //                { "empresa_anterior", cita.Empresa_anterior },
        //                { "actividad_empresa_actual", cita.Actividad_empresa_actual },
        //                { "actividad_empresa_anterior", cita.Actividad_empresa_anterior },
        //                { "empleador", cita.Empleador },
        //                { "lugar_nacimiento", cita.Lugar_nacimiento },
        //                { "unidad", cita.Unidad },
        //                { "zona", cita.Zona },
        //                { "area", cita.Area },
        //                { "puesto", cita.Puesto },
        //                { "protocolo", cita.Protocolo },
        //                { "titular", cita.Titular },
        //                { "evaluador", cita.Evaluador },
        //                { "tipo_examen", cita.Tipo_examen },
        //                { "fecha_levante_observacion", cita.Fecha_levante_observacion },
        //                { "nombres_apellidos", cita.Nombres_apellidos },
        //                { "apellidos_nombres", cita.Apellidos_nombres },
        //                { "dni", cita.Dni },
        //                { "empresa", cita.Empresa },
        //                { "fecha_evaluacion", cita.Fecha_evaluacion },
        //                { "fecha", cita.Fecha },
        //                { "fecha_nacimiento", cita.Fecha_nacimiento },
        //                { "masculino", cita.Masculino },
        //                { "femenino", cita.Femenino },
        //                { "tipo_documento", cita.Tipo_documento },
        //                { "telefono", cita.Telefono },
        //                { "hcl", cita.Hcl },
        //                { "edad", cita.Edad },
        //                { "grado_instruccion", cita.Grado_instruccion },
        //                { "estado_civil", cita.Estado_civil },
        //                { "lugar_residencia", cita.Lugar_residencia },
        //                { "tiempo_total_laborando", cita.Tiempo_total_laborando?.ToString() },
        //                { "area_trabajo_superficie", cita.Area_trabajo_superficie },
        //                { "area_trabajo_subsuelo", cita.Area_trabajo_subsuelo },
        //                { "apellidos", cita.Apellidos },
        //                { "nombres", cita.Nombres },
        //                { "examen", cita.Examen },

        //                { "evaluacion_preocupacional", cita.Evaluacion_preocupacional },
        //                { "evaluacion_ocupacional", cita.Evaluacion_ocupacional },
        //                { "evaluacion_postocupacional", cita.Evaluacion_postocupacional },
        //                { "c_ps_p", cita.C_ps_p },
        //                { "nacionalidad", cita.Nacionalidad },
        //                { "sexo", cita.Sexo },
        //                { "departamento", cita.Departamento },
        //                { "tiempo_experiencia", cita.Tiempo_experiencia.ToString() },
        //                { "compañia", cita.Compañia },
        //                { "tipo_licencia", cita.Tipo_licencia },
        //                { "codigo", cita.Codigo },
        //                { "version", cita.Version },
        //                { "evaluacion_periodica", cita.Evaluacion_periodica },
        //                { "nombre_eess", cita.Nombre_eess },
        //                { "direccion", cita.Direccion },
        //                { "contacto", cita.Contacto },
        //                { "hora_evaluacion", cita.Hora_evaluacion },
        //                { "lugar_evaluacion", cita.Lugar_evaluacion },
        //                { "domicilio", cita.Domicilio },
        //                { "soltero", cita.Soltero },
        //                { "casado", cita.Casado },
        //                { "conviviente", cita.Conviviente },
        //                { "viudo", cita.Viudo },
        //                { "divorciado", cita.Divorciado },
        //                { "analfabeto", cita.Analfabeto },
        //                { "prim_completa", cita.Prim_completa },
        //                { "prim_incompleta", cita.Prim_incompleta },
        //                { "sec_completa", cita.Sec_completa },
        //                { "sec_incompleta", cita.Sec_incompleta },
        //                { "tecnico", cita.Tecnico },
        //                { "universitario", cita.Universitario },
        //                { "mestizo", cita.Mestizo },
        //                { "andino", cita.Andino },
        //                { "asiatico_descendiente", cita.Asiatico_descendiente },
        //                { "indigena_amazonico", cita.Indigena_amazonico },
        //                { "afrodescendiente", cita.Afrodescendiente },
        //                { "otra_etnia_raza", cita.Otra_etnia_raza },
        //                { "otra_etnia_raza_descripcion", cita.Otra_etnia_raza_descripcion },
        //                { "pueblo_etnico", cita.Pueblo_etnico },
        //                { "peruano", cita.Peruano },
        //                { "extranjero", cita.Extranjero },
        //                { "pais_nacionalidad", cita.Pais_nacionalidad },
        //                { "migrante_si", cita.Migrante_si },
        //                { "migrante_no", cita.Migrante_no },
        //                { "pais_origen", cita.Pais_origen },
        //                { "residencia_pais", cita.Residencia_pais },
        //                { "residencia_localidad", cita.Residencia_localidad },
        //                { "residencia_urb_area", cita.Residencia_urb_area },
        //                { "residencia_tipo_via", cita.Residencia_tipo_via },
        //                { "residencia_lote_nr", cita.Residencia_lote_nr },
        //                { "residencia_nombre_via", cita.Residencia_nombre_via },
        //                { "residencia_departamento", cita.Residencia_departamento },
        //                { "residencia_provincia", cita.Residencia_provincia },
        //                { "residencia_distrito", cita.Residencia_distrito },
        //                { "mayor_65_años", cita.Mayor_65_años },
        //                { "lugar_pais_visita", cita.Lugar_pais_visita },
        //                { "correo", cita.Correo },
        //                { "datos_contacto_emergencia", cita.Datos_contacto_emergencia },
        //                { "contratista", cita.Contratista },
        //                { "n_ficha", cita.N_ficha },
        //                { "zona_labor_concentradora", cita.Zona_labor_concentradora },
        //                { "zona_labor_subsuelo", cita.Zona_labor_subsuelo },
        //                { "altitud_labor_debajo_2500m", cita.Altitud_labor_debajo_2500m },
        //                { "altitud_labor_hasta_3000", cita.Altitud_labor_hasta_3000 },
        //                { "altitud_labor_3001m_5000m", cita.Altitud_labor_3001m_5000m },
        //                { "altitud_labor_3501m_4000m", cita.Altitud_labor_3501m_4000m },
        //                { "altitud_labor_4001m_4500m", cita.Altitud_labor_4001m_4500m },
        //                { "altitud_labor_mas_4501m", cita.Altitud_labor_mas_4501m },
        //                { "año", cita.Año },
        //                { "mes", cita.Mes },
        //                { "dia", cita.Dia },
        //                { "empresa_salud", cita.Empresa_salud },
        //                { "ultima_fecha_visita_bambas", cita.Ultima_fecha_visita_bambas },
        //                { "nombre_correo_supervisor", cita.Nombre_correo_supervisor },
        //                { "ruc", cita.Ruc }
        //            };

        //            if (bIncluirFirma)
        //            {
        //                datos.Add("firma", firma);
        //            }

        //            foreach (KeyValuePair<string, string> item in datos)
        //            {
        //                htmlContent = htmlContent.Replace("{" + item.Key + "}", item.Value);
        //            }

        //            /************************ body ************************/

        //            var valores = cita_data.Select(x => new ValorDato { CodigoDato = x.VcCodigoDato, CodigoFicha = x.VcCodigoFicha, CodigoPrueba = x.VcCodigoPrueba, 
        //                Valor = (!string.IsNullOrEmpty(x.VcValorDescripcion) ? x.VcValorDescripcion : x.BEsDecimal == true ? x.DeValor?.ToString() : x.VcValor) }).ToList();
        //            var variables = new Dictionary<string, string>();

        //            var cie10 = await _sunat.ListCie10(vcCodEpisodioSync,true);
        //            var cie9 = await _sunat.ListCie10(vcCodEpisodioSync, false);
        //            var medicamentos = await _sunat.ListHabitos(vcCodEpisodioSync, 2);
        //            var riesgos = await _sunat.ListRiesgos(vcCodEpisodioSync);
        //            var epps = await _sunat.ListEPPs(vcCodEpisodioSync);
        //            var vacunas = await _sunat.ListVacunas(vcCodEpisodioSync);
        //            var alergias = await _sunat.ListAlergias(vcCodEpisodioSync);



        //            foreach (var variable in jsonParam.pruebas)
        //            {
        //                var valorVariable = GetValorVariable(variable, valores, cie10, cie9,cita.SiGenero, medicamentos, riesgos, epps, vacunas, alergias);
        //                variables.Add(variable.nombre, valorVariable);
        //            }

        //            foreach (KeyValuePair<string, string> item in variables)
        //            {
        //                htmlContent = htmlContent.Replace("{" + item.Key + "}", item.Value);
        //            }

        //            byte[] outPdfBuffer = _pdfservice.GenerarPdfStream(htmlContent);

        //            var dFecha = DateTime.Now.ToString("MMyyyy");

        //            var filenamecloud = @"" + dFecha + "/" + guid + "_" + vcCodEpisodioSync + ".pdf";
        //            var pathcloud = await _cloudStorage.UploadFileAsync(new MemoryStream(outPdfBuffer), filenamecloud);

        //            DocFin doc = new()
        //            {
        //                vcNombre = nombreDocumento,
        //                vcRuta = pathcloud,
        //                iIdSubClaseDocumento = iIdSubClaseDocumento,
        //                idUsuario = 1,
        //                nvGUID = guid,
        //                vcExtension = ".pdf"
        //            };

        //            var iddoc = await _sunat.InsertaDocumento(doc);

        //            await _sunat.InsertaEntregableaEpisodio(doc, iddoc, vcCodEpisodioSync);

        //            data = new
        //            {
        //                iddoc,
        //                guid
        //            };

        //            // 6.- Enviar a API para cada entregable
        //            try
        //            {
        //                string fileBase64 = Convert.ToBase64String(outPdfBuffer);

        //                //Identificador = 1,
        //                object body = new
        //                {
        //                    Base64PDF = fileBase64,
        //                    NroEpisodio = vcCodEpisodioSync,
        //                    NombrePDF = cita.VcDocumentoNombre,
        //                    Observacion = cita.VcDocumentoCodigo,
        //                    Clase = clase
        //                };

        //                //_logger.LogInformation(Newtonsoft.Json.JsonConvert.SerializeObject(body));

        //                var _request = SendApiDocument(body);

        //                object request = new
        //                {
        //                    vcCodEpisodioSync,
        //                    _request.statusCode,
        //                    _request.resultContent
        //                };

        //                _logger.LogInformation(Newtonsoft.Json.JsonConvert.SerializeObject(request));
        //            }
        //            catch (Exception ex)
        //            {
        //                _logger.LogError("inicio - error enviado el doc a chaman clase:"+ clase);
        //                //_logger.LogError(ex.StackTrace);
        //                _logger.LogError(ex.Message);
        //                //_logger.LogError("fin - error enviado el doc a chaman clase:" + clase);
        //            }
        //        }
        //        else
        //        {
        //            throw new Exception($"No existe la cita trabajador {iIdCitaTrabaTitular}");
        //        }

        //        return data;
        //    }

        //    private (HttpStatusCode statusCode, string resultContent) SendApiDocument(dynamic body)
        //    {
        //        return Helper.Request(body, _urlApiDocumento, null, "post");
        //    }

        //    private async Task<string> GetLogoBase64(bool logopersonalizado, int iIdCitaTrabaTitular)
        //    {
        //        byte[] file;

        //        if (logopersonalizado == true)
        //        {
        //            file = await _sunat.getLogoEmpresaXCita(iIdCitaTrabaTitular);
        //        }
        //        else
        //        {
        //            WebClient myWebClient = new();
        //            file = await myWebClient.DownloadDataTaskAsync(_logoPath);
        //        }

        //        string logo = "";

        //        if (file != null)
        //        {
        //            MemoryStream stream = new(file) { Position = 0 };
        //            logo = Convert.ToBase64String(stream.ToArray());
        //        }

        //        return logo;
        //    }

        //    private string GetValorVariable(Prueba prueba, IList<ValorDato> valores, IEnumerable<Antecedente> cie10, IEnumerable<Antecedente> cie9,short sexo, IEnumerable<Antecedente> medicamentos, IEnumerable<Antecedente> riesgos, IEnumerable<Antecedente> epps, IEnumerable<Antecedente> vacunas, IEnumerable<Antecedente> alergias)
        //    {
        //        IList<string> valores_variable = new List<string>();

        //        if (prueba.campos.Count == 1)
        //        {
        //            var campo = prueba.campos[0];
        //            var valor = valores.Where(x => x.CodigoFicha == campo.path.codigo_ficha && x.CodigoDato == campo.path.codigo_campo).Select(x => x.Valor).FirstOrDefault();

        //            if (!string.IsNullOrEmpty(valor))
        //            {
        //                string[] subs = valor.Split("||");

        //                if (campo.valores.Count == 0)
        //                {
        //                    // a.- dato simple

        //                    valores_variable.Add(valor);
        //                }
        //                else
        //                {
        //                    // b.- dato con opciones
        //                    foreach(var itemval in subs) { 
        //                        if (campo.valores.Contains(itemval.Trim()))
        //                        {
        //                            valores_variable.Add("X");
        //                            break;
        //                        }

        //                    }
        //                }
        //            }
        //            else
        //            {
        //                // para casos a y b

        //                valores_variable.Add(prueba.valorxdefecto);
        //            }
        //        }

        //        //****************************COMBINADOS********************************

        //        if (prueba.campos.Count > 1)
        //        {
        //            // c.- Dato con varios posibles valores (combinados)

        //            foreach (var campo in prueba.campos)
        //            {
        //                var valor = valores.Where(x => x.CodigoFicha == campo.path.codigo_ficha && x.CodigoDato == campo.path.codigo_campo).Select(x => x.Valor).FirstOrDefault();


        //                if (!string.IsNullOrEmpty(valor))
        //                {
        //                    string[] subs = valor.Split("||");

        //                    if (campo.valores.Count == 0 && (!campo.path.sexo.HasValue || campo.path.sexo == sexo))
        //                    {
        //                        // a.- dato simple

        //                        valores_variable.Add(valor);
        //                    }
        //                    else
        //                    {
        //                        // b.- dato con opciones
        //                        foreach (var itemval in subs)
        //                        {
        //                            if (campo.valores.Contains(itemval.Trim()) && (!campo.path.sexo.HasValue || campo.path.sexo == sexo))
        //                            {
        //                                valores_variable.Add("X");
        //                                break;
        //                            }
        //                        }
        //                    }
        //                }
        //            }
        //        }
        //        //****************************CIE 10********************************
        //        if (prueba.cie10.Count > 0 && (!prueba.cie10operacionexiste.HasValue || prueba.cie10operacionexiste == true))
        //        {
        //            // d.- Dato con CIE10

        //            foreach (var val in prueba.cie10)
        //            {

        //                if (cie10.Any(item => item.codigo == val && item.siTipoFamiliar == 0))
        //                {
        //                    valores_variable.Add("X");
        //                    break;
        //                }
        //            }
        //        }

        //        if (prueba.cie10.Count > 0 && prueba.cie10operacionexiste.HasValue && prueba.cie10operacionexiste == false)
        //        {
        //            // g.- Dato con CIE10

        //            foreach (var val in prueba.cie10)
        //            {
        //                if (!cie10.Any(item => item.codigo == val && item.siTipoFamiliar == 0))
        //                {
        //                    valores_variable.Add("X");
        //                    break;
        //                }
        //            }
        //        }

        //        if (prueba.cie10siexiste.Count > 0)
        //        {
        //            // m.- Dato con descripcion

        //            foreach (var val in prueba.cie10siexiste)
        //            {
        //                if (cie10.Any(item => item.codigo == val && item.siTipoFamiliar == 0))
        //                {
        //                    valores_variable.Add(cie10.FirstOrDefault(item => item.codigo == val && item.siTipoFamiliar == 0).valor);
        //                }
        //            }
        //        }
        //        //****************************CIE 9********************************
        //        if (prueba.cie9.Count > 0 && (!prueba.cie9operacionexiste.HasValue || prueba.cie9operacionexiste == true))
        //        {
        //            // d.- Dato con CIE9

        //            foreach (var val in prueba.cie9)
        //            {
        //                if (cie9.Any(item => item.codigo == val && item.siTipoFamiliar == 0))
        //                {
        //                    valores_variable.Add("X");
        //                    break;
        //                }
        //            }
        //        }

        //        if (prueba.cie9.Count > 0 && prueba.cie9operacionexiste.HasValue && prueba.cie9operacionexiste == false)
        //        {
        //            // g.- Dato con CIE9

        //            foreach (var val in prueba.cie9)
        //            {
        //                if (!cie9.Any(item => item.codigo == val && item.siTipoFamiliar == 0))
        //                {
        //                    valores_variable.Add("X");
        //                    break;
        //                }
        //            }
        //        }

        //        if (prueba.cie9siexiste.Count > 0)
        //        {
        //            // m.- Dato con descripcion

        //            foreach (var val in prueba.cie9siexiste)
        //            {
        //                if (cie10.Any(item => item.codigo == val && item.siTipoFamiliar == 0))
        //                {
        //                    valores_variable.Add(cie10.FirstOrDefault(item => item.codigo == val && item.siTipoFamiliar == 0).valor);
        //                }
        //            }
        //        }

        //        //****************************MEDICAMENTOS********************************
        //        if (prueba.medicamentos.Count > 0)
        //        {
        //            // i.- Dato con descripcion

        //            foreach (var val in prueba.medicamentos)
        //            {
        //                if (medicamentos.Any(item => item.codigo == val))
        //                {
        //                    valores_variable.Add(medicamentos.FirstOrDefault(item => item.codigo == val).valor);
        //                }
        //            }
        //        }

        //        //****************************riesgos********************************
        //        if (prueba.riesgos.Count > 0)
        //        {
        //            // i.- Dato con descripcion

        //            foreach (var val in prueba.riesgos)
        //            {
        //                if (riesgos.Any(item => item.codigo == val))
        //                {
        //                    valores_variable.Add(riesgos.FirstOrDefault(item => item.codigo == val).valor);
        //                }
        //            }
        //        }

        //        //****************************EPP********************************
        //        if (prueba.medidas_proteccion.Count > 0)
        //        {
        //            // i.- Dato con descripcion

        //            foreach (var val in prueba.medidas_proteccion)
        //            {
        //                if (epps.Any(item => item.codigo == val))
        //                {
        //                    valores_variable.Add(epps.FirstOrDefault(item => item.codigo == val).valor);
        //                }
        //            }
        //        }

        //        //****************************vacunas********************************
        //        if (prueba.vacunas.Count > 0)
        //        {
        //            // i.- Dato con descripcion

        //            foreach (var val in prueba.vacunas)
        //            {
        //                if (vacunas.Any(item => item.codigo == val))
        //                {
        //                    valores_variable.Add(vacunas.FirstOrDefault(item => item.codigo == val).valor);
        //                }
        //            }
        //        }

        //        //****************************alergias********************************
        //        if (prueba.alergias.Count > 0)
        //        {
        //            // i.- Dato con descripcion

        //            foreach (var val in prueba.alergias)
        //            {
        //                if (alergias.Any(item => item.codigo == val))
        //                {
        //                    valores_variable.Add(alergias.FirstOrDefault(item => item.codigo == val).valor);
        //                }
        //            }
        //        }

        //        //****************************ANTECEDENTES FAMILIARES**********************************
        //        if (prueba.antecedentefamiliar != null)
        //        {
        //            if (prueba.antecedentefamiliar.cie10.Count > 0 && (!prueba.antecedentefamiliar.cie10existe.HasValue || prueba.antecedentefamiliar.cie10existe == true))
        //            {
        //                // d.- Dato con CIE9

        //                foreach (var val in prueba.antecedentefamiliar.cie10)
        //                {
        //                    if (cie10.Any(item => item.codigo == val && item.siTipoFamiliar == prueba.antecedentefamiliar.tipofamiliar))
        //                    {
        //                        valores_variable.Add("X");
        //                        break;
        //                    }
        //                }
        //            }

        //            if (prueba.antecedentefamiliar.cie10.Count > 0 && prueba.antecedentefamiliar.cie10existe.HasValue && prueba.antecedentefamiliar.cie10existe == false)
        //            {
        //                // g.- Dato con CIE9

        //                foreach (var val in prueba.antecedentefamiliar.cie10)
        //                {
        //                    if (!cie10.Any(item => item.codigo == val && item.siTipoFamiliar == prueba.antecedentefamiliar.tipofamiliar))
        //                    {
        //                        valores_variable.Add("X");
        //                        break;
        //                    }
        //                }
        //            }
        //        }

        //        //****************************EXISTE PRUEBA***************************************
        //        if (prueba.existeprueba != null)
        //        {
        //            foreach (var pruebajson in prueba.existeprueba.prueba)
        //            {
        //               if( valores.Any(x => x.CodigoPrueba == pruebajson))
        //                {
        //                    valores_variable.Add("table");
        //                    break;
        //                }
        //            }

        //            if(valores_variable.Count == 0) valores_variable.Add("none");
        //        }

        //        //****************************PRESENCIA ABSOLUTA***************************************
        //        if (prueba.presencia != null)
        //        {
        //            int cantidad = prueba.presencia.campos.Count;
        //            int contador = 0;
        //            if (prueba.presencia.campos.Count > 0)
        //            {
        //                // c.- Dato con varios posibles valores (combinados)

        //                foreach (var campo in prueba.presencia.campos)
        //                {
        //                    var valor = valores.Where(x => x.CodigoFicha == campo.path.codigo_ficha && x.CodigoDato == campo.path.codigo_campo).Select(x => x.Valor).FirstOrDefault();


        //                    if (!string.IsNullOrEmpty(valor))
        //                    {
        //                        string[] subs = valor.Split("||");

        //                        if (subs.Any(item => item.Trim() == prueba.presencia.valor_comparar) && campo.valores.Count == 0 && (!campo.path.sexo.HasValue || campo.path.sexo == sexo))
        //                        {
        //                            // a.- dato simple
        //                            contador++;
        //                            //valores_variable.Add(valor);
        //                        }
        //                        else
        //                        {
        //                            // b.- dato con opciones
        //                            foreach (var itemval in subs)
        //                            {
        //                                if (prueba.presencia.valor_comparar.Equals(itemval.Trim()) && campo.valores.Contains(itemval.Trim()) && (!campo.path.sexo.HasValue || campo.path.sexo == sexo))
        //                                {
        //                                    contador++;
        //                                    //valores_variable.Add("X");
        //                                    break;
        //                                }
        //                            }
        //                        }
        //                    }
        //                }
        //            }

        //            if(contador == cantidad) valores_variable.Add(prueba.presencia.valor_presente);
        //            else valores_variable.Add(prueba.presencia.valor_ausente);

        //        }
        //        //****************************LABO***************************************

        //        if (prueba.labo !=null)
        //        {
        //            // e.- Caso pruebas de laboratorio

        //                if (prueba.labo.pruebas.Count > 0 && prueba.labo.valores.Count > 0)
        //                {
        //                    foreach(var pruebajson in prueba.labo.pruebas) { 
        //                        var valor = valores.Where(x => x.CodigoPrueba == pruebajson).Select(x => x.Valor).FirstOrDefault();
        //                        if (!string.IsNullOrEmpty(valor))
        //                        {
        //                            string[] subs = valor.Split("||");
        //                            foreach (var itemval in subs)
        //                            {
        //                                if (prueba.labo.valores.Contains(itemval.Trim()))
        //                                {
        //                                    valores_variable.Add("X");
        //                                    break;
        //                                }
        //                            }
        //                        }
        //                    }

        //                    }else if(prueba.labo.pruebas.Count > 0) {
        //                        foreach (var pruebajson in prueba.labo.pruebas)
        //                        {
        //                            var valor = valores.Where(x => x.CodigoPrueba == pruebajson).Select(x => x.Valor).FirstOrDefault();

        //                            if (!string.IsNullOrEmpty(valor))
        //                            {
        //                                valores_variable.Add(valor);
        //                            }
        //                        }
        //                    }
        //        }

        //        // f.- Caso valores a comparar.

        //        foreach (var campo in prueba.campos)
        //        {
        //            var valor = valores.Where(x => x.CodigoFicha == campo.path.codigo_ficha && x.CodigoDato == campo.path.codigo_campo).Select(x => x.Valor).FirstOrDefault();

        //            if (!string.IsNullOrEmpty(valor))
        //            {
        //                string[] subs = valor.Split("||");
        //                foreach (var itemval in subs)
        //                {
        //                    //valor = valor.Trim();
        //                    try
        //                    {
        //                        string resultEval = campo.operacion.operador?.Trim() switch
        //                        {
        //                            "1" => campo.operacion.valor_comparacion == itemval.Trim() ? "X" : "",
        //                            "2" => Convert.ToDecimal(campo.operacion.valor_comparacion) >= Convert.ToDecimal(itemval.Trim()) ? "X" : "",
        //                            "3" => Convert.ToDecimal(campo.operacion.valor_comparacion) <= Convert.ToDecimal(itemval.Trim()) ? "X" : "",
        //                            "4" => Convert.ToDecimal(campo.operacion.valor_comparacion) > Convert.ToDecimal(itemval.Trim()) ? "X" : "",
        //                            "5" => Convert.ToDecimal(campo.operacion.valor_comparacion) < Convert.ToDecimal(itemval.Trim()) ? "X" : "",
        //                            "6" => campo.operacion.valor_comparacion != itemval.Trim() ? "X" : "",
        //                            _ => "",
        //                        };

        //                        if (!string.IsNullOrEmpty(resultEval))
        //                        {
        //                            valores_variable.Add(resultEval);
        //                        }
        //                    }
        //                    catch (Exception ex) { _logger.LogError(ex.Message); }
        //                }
        //            }
        //        }

        //        string valorVariable = string.Join("", valores_variable);

        //        return valorVariable;
        //    }
        //}

        //internal class ValorDato
        //{
        //    public string CodigoDato { get; set; }
        //    public string CodigoFicha { get; set; }
        //    public string Valor { get; set; }
        //    public string CodigoPrueba { get; set; }
        //}
        //internal class Variable
        //{
        //    public Variable()
        //    {
        //        pruebas = new List<Prueba>();
        //    }
        //    public object datos_paciente { get; set; }
        //    public DatosDocumento datos_documento { get; set; }
        //    public object datos_protocolo { get; set; }
        //    public IList<Prueba> pruebas { get; set; }
        //}
        //internal class DatosDocumento
        //{
        //    public string link { get; set; }
        //    public string qrcode { get; set; }
        //    public string firma { get; set; }
        //    public string logopersonalizado { get; set; }
        //}
        //internal class Presencia
        //{
        //    public string valor_comparar { get; set; }
        //    public string valor_presente { get; set; }
        //    public string valor_ausente { get; set; }
        //    public IList<Campo> campos { get; set; }
        //}

        //internal class Antecedentefamiliar
        //{
        //    public Antecedentefamiliar()
        //    {
        //        cie10 = new List<string>();
        //    }
        //    public short tipofamiliar { get; set; }
        //    public IList<string> cie10 { get; set; }
        //    public bool? cie10existe { get; set; }
        //}

        //internal class Existeprueba
        //{
        //    public Existeprueba()
        //    {
        //        prueba = new List<string>();
        //    }
        //    public IList<string> prueba { get; set; }
        //}

        //internal class Prueba
        //{
        //    public Prueba()
        //    {
        //        campos = new List<Campo>();
        //        cie10 = new List<string>();
        //        cie9 = new List<string>();
        //        cie10siexiste = new List<string>();
        //        cie9siexiste = new List<string>();
        //        vacunas = new List<string>();
        //        alergias = new List<string>();
        //        medicamentos = new List<string>();
        //        riesgos = new List<string>();
        //        medidas_proteccion = new List<string>();
        //    }

        //    public string nombre { get; set; }
        //    public string valorxdefecto { get; set; }
        //    public Presencia presencia { get; set; }
        //    public Existeprueba existeprueba { get; set; }
        //    public Antecedentefamiliar antecedentefamiliar { get; set; }
        //    public IList<Campo> campos { get; set; }
        //    public IList<string> medicamentos { get; set; }
        //    public IList<string> riesgos { get; set; }
        //    public IList<string> medidas_proteccion { get; set; }
        //    public IList<string> vacunas { get; set; }
        //    public IList<string> alergias { get; set; }
        //    public IList<string> cie10 { get; set; }
        //    public IList<string> cie9 { get; set; }
        //    public IList<string> cie10siexiste { get; set; }
        //    public IList<string> cie9siexiste { get; set; }
        //    public bool? cie10operacionexiste { get; set; }
        //    public bool? cie9operacionexiste { get; set; }
        //    //public IList<string> labo { get; set; }
        //    public Labo labo { get; set; }
        //}
        //internal class Campo
        //{
        //    public Campo()
        //    {
        //        valores = new List<string>();
        //    }

        //    public Path path { get; set; }
        //    public Operacion operacion { get; set; }
        //    public IList<string> valores { get; set; }
        //}
        //internal class Path
        //{
        //    public string codigo_ficha { get; set; }
        //    public string codigo_campo { get; set; }
        //    public short? sexo { get; set; }
        //}
        //internal class Operacion
        //{
        //    public string operador { get; set; }
        //    public string valor_comparacion { get; set; }
        //}
        //internal class Labo
        //{
        //    public Labo()
        //    {
        //        pruebas = new List<string>();
        //        valores = new List<string>();
        //    }

        //    public IList<string> pruebas { get; set; }
        //    public IList<string> valores { get; set; }
        //}
    }
}
