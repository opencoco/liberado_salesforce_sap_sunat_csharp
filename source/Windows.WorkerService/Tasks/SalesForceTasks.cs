using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using ACME.Data.Contracts;
using ACME.Data.Entity;
using ACME.WorkerService.Helpers;
using ACME.WorkerService.Tools;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace ACME.WorkerService.Tasks
{
    internal class SalesForceTasks : ISalesForceTasks
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger _logger;
        private readonly IBNVManager _bnv;
        private readonly bool procesarSF;
        public SalesForceTasks(IBNVManager bnv, ILogger<SalesForceTasks> logger, IConfiguration configuration)
        {
            _configuration = configuration;
            _logger = logger;
            _bnv = bnv;
            procesarSF = (_configuration["SalesForce:Procesar"].ToString()).Equals("SI") ? true : false;
        }


        public async Task<object> SF_EnviaBajaPacientes()
        {
            if (procesarSF)
            {
                string IdPaciente = "";
                _logger.LogInformation("-----> SF_EnviaBajaPacientes inicio");

                //TODOS
                try
                {
                    //SF Init
                    string LoginEndpoint = @_configuration["SalesForce:LoginEndpoint"];

                    string Username = @_configuration["SalesForce:Username"];
                    string Password = @_configuration["SalesForce:Password"];
                    string ClientId = @_configuration["SalesForce:ClientId"];
                    string ClientSecret = @_configuration["SalesForce:ClientSecret"];
                    string AuthToken = "";
                    string Token = "";
                    string ServiceUrl = "";

                    HttpClient Client;
                    Client = new HttpClient();

                    HttpContent content = new FormUrlEncodedContent(new Dictionary<string, string>
                                  {
                                      {"grant_type", "password"},
                                      {"client_id", ClientId},
                                      {"client_secret", ClientSecret},
                                      {"username", Username},
                                      {"password", Password}
                                  });

                    HttpResponseMessage message = Client.PostAsync(LoginEndpoint, content).Result;

                    string response = message.Content.ReadAsStringAsync().Result;
                    JObject obj = JObject.Parse(response);

                    AuthToken = (string)obj["access_token"];
                    ServiceUrl = (string)obj["instance_url"];
                    //END SF Init

                    var pacientes = await _bnv.SF_GetBajaPacientesPendientes();
                    foreach (var pac in pacientes)
                    {
                        try
                        {
                            var listDocument = new List<object>();
                            listDocument.Add(new
                            {
                                method = "PATCH",
                                url = $"/services/data/v56.0/sobjects/Account/Id/{pac.Id_paciente}",
                                referenceId = "refPaciente",
                                body = new
                                {
                                    Cesado__c = 1,
                                    Fecha_cesado__c = DateTime.ParseExact(pac.Fechasalida, "dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd")
                                }
                            });

                            var param = new
                            {
                                allOrNone = "false",
                                compositeRequest = listDocument
                            };

                            string url = $"{ServiceUrl}/services/data/v56.0/composite/";
                            string method = "POST";
                            string contentType = "application/json; charset=utf-8";
                            string accessToken = AuthToken;

                            _logger.LogInformation("Salesforce - Account BAJA");

                            string body = JsonConvert.SerializeObject(param);

                            _logger.LogInformation(body);

                            byte[] result = Helper.executeRequestSalesForce(url: url
                            , method: method
                            , contentType: contentType
                            , accessToken: accessToken
                            , Encoding.UTF8.GetBytes(body));

                            if (result != null && result.Length > 0)
                            {
                                var resstr = Encoding.UTF8.GetString(result);
                                try
                                {
                                    JObject ressf = JObject.Parse(resstr);
                                    var bsuccess = resstr.Contains("true");
                                    if (bsuccess) await _bnv.SF_BajaPacienteUpd(pac.Id_paciente);
                                }
                                catch (Exception ex)
                                {
                                    throw new Exception(resstr);
                                }
                            }
                            else
                            {
                                throw new Exception("Error al conectarse a SF");
                            }




                        }
                        catch (Exception ex)
                        {
                            _logger.LogError(ex.Message + $", id_paciente: {pac.Id_paciente}");
                            //await _bnv.SF_InsertError(ent.DNI, ent.IdEmpresa, ex.Message);
                        }
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex.Message);
                    //await _bnv.SF_InsertError(ent.DNI, ent.IdEmpresa, ex.Message);
                }
                _logger.LogInformation("-----> SF_EnviaBajaPacientes fin");
            }
            return true;
        }

        public async Task<object> SF_SincronizaHeadcount()
        {
            if (procesarSF)
            {
                _logger.LogInformation("-----> SF_SincronizaHeadcount inicio");
                try
                {
                    await _bnv.SF_SincronizaHeaCount();
                }
                catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            _logger.LogInformation("-----> SF_SincronizaHeadcount fin");
        }
            return true;
        }




    public async Task<object> SF_ActualizaCita()
        {
            if (procesarSF)
            {
                string IdPaciente = "";
                _logger.LogInformation("-----> SF_ActualizaCita inicio");

                //TODOS
                try
                {
                    //SF Init
                    string LoginEndpoint = @_configuration["SalesForce:LoginEndpoint"];

                    string Username = @_configuration["SalesForce:Username"];
                    string Password = @_configuration["SalesForce:Password"];
                    string ClientId = @_configuration["SalesForce:ClientId"];
                    string ClientSecret = @_configuration["SalesForce:ClientSecret"];
                    string AuthToken = "";
                    string Token = "";
                    string ServiceUrl = "";

                    HttpClient Client;
                    Client = new HttpClient();

                    HttpContent content = new FormUrlEncodedContent(new Dictionary<string, string>
                                  {
                                      {"grant_type", "password"},
                                      {"client_id", ClientId},
                                      {"client_secret", ClientSecret},
                                      {"username", Username},
                                      {"password", Password}
                                  });

                    HttpResponseMessage message = Client.PostAsync(LoginEndpoint, content).Result;

                    string response = message.Content.ReadAsStringAsync().Result;
                    JObject obj = JObject.Parse(response);

                    AuthToken = (string)obj["access_token"];
                    ServiceUrl = (string)obj["instance_url"];
                    //END SF Init

                    var citas = await _bnv.SF_GetUpdateCitas();
                    foreach (var cita in citas)
                    {
                        try
                        {
                            

                            var param = new
                            {
                                Status = cita.Status,
                                SourceSystemIdentifier = cita.SourceSystemIdentifier,
                                StartDate = cita.StartDate,
                                Establecimiento__c = cita.Establecimiento__c,
                                Priority = cita.Priority,
                                Estado_del_pago_de_la_cita__c = cita.Estado_del_pago_de_la_cita__c,
                                DateSigned = cita.DateSigned,
                                Estado_de_la_atencion__c = cita.Estado_de_la_atencion__c
                            };

                            string url = $"{ServiceUrl}/services/data/v56.0/sobjects/ClinicalServiceRequest/OrdenMedicaId__c/{cita.id_orden}";
                            string method = "PATCH";
                            string contentType = "application/json; charset=utf-8";
                            string accessToken = AuthToken;

                            _logger.LogInformation($"Salesforce - actualizando cita orden {cita.id_orden}");

                            string body = JsonConvert.SerializeObject(param);
                            string bodylog = JsonConvert.SerializeObject(param, Newtonsoft.Json.Formatting.Indented);

                            _logger.LogInformation(bodylog);

                            byte[] result = Helper.executeRequestSalesForce(url: url
                            , method: method
                            , contentType: contentType
                            , accessToken: accessToken
                            , Encoding.UTF8.GetBytes(body));

                            if (result != null && result.Length > 0)
                            {
                                var resstr = Encoding.UTF8.GetString(result);
                                try
                                {
                                    _logger.LogInformation($"Salesforce - actualizando cita orden {cita.id_orden} respuesta {resstr}");
                                    JObject ressf = JObject.Parse(resstr);
                                    bool bsuccess = bool.Parse(ressf["success"]?.ToString());
                                    //IdPaciente = ressf["id"]?.ToString();
                                    //IdPaciente = idpaciente;
                                    if (bsuccess)
                                    {
                                        //sfpersona.IdPaciente = IdPaciente;
                                    }
                                    else
                                    {
                                        throw new Exception(resstr);
                                    }
                                }
                                catch (Exception ex)
                                {
                                    throw new Exception(resstr);
                                }
                            }
                            else
                            {
                                throw new Exception("Error al conectarse a SF");
                            }


                            //update registro
                            await _bnv.SF_OrdenMEdicaNotiUpd(cita.id_orden);


                        }
                        catch (Exception ex)
                        {
                            _logger.LogError(ex.Message + $", sf order id: {cita.id_orden}");
                        }
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex.Message);
                    //await _bnv.SF_InsertError(ent.DNI, ent.IdEmpresa, ex.Message);
                }
                _logger.LogInformation("-----> SF_ActualizaCita fin");
            }
            return true;
        }
        public async Task<object> SF_EnviasDatosAltaPaciente()
        {
            if (procesarSF)
            {
                string IdPaciente = "";
                _logger.LogInformation("-----> SF_EnviasDatosAltaPaciente inicio");

                //TODOS
                try
                {
                    //SF Init
                    string LoginEndpoint = @_configuration["SalesForce:LoginEndpoint"];

                    string Username = @_configuration["SalesForce:Username"];
                    string Password = @_configuration["SalesForce:Password"];
                    string ClientId = @_configuration["SalesForce:ClientId"];
                    string ClientSecret = @_configuration["SalesForce:ClientSecret"];
                    string AuthToken = "";
                    string Token = "";
                    string ServiceUrl = "";

                    HttpClient Client;
                    Client = new HttpClient();

                    HttpContent content = new FormUrlEncodedContent(new Dictionary<string, string>
                                  {
                                      {"grant_type", "password"},
                                      {"client_id", ClientId},
                                      {"client_secret", ClientSecret},
                                      {"username", Username},
                                      {"password", Password}
                                  });

                    HttpResponseMessage message = Client.PostAsync(LoginEndpoint, content).Result;

                    string response = message.Content.ReadAsStringAsync().Result;
                    JObject obj = JObject.Parse(response);

                    AuthToken = (string)obj["access_token"];
                    ServiceUrl = (string)obj["instance_url"];
                    //END SF Init

                    var pacientes = await _bnv.SF_GetPacientesPendientesDeAlta();
                    foreach (var dni in pacientes)
                    {
                        try
                        {
                            var sfpersona = await _bnv.SF_GetPacienteAlta(dni);
                            if (sfpersona == null) throw new Exception("No existe la persona en la BD_ESTADISTICA");
                            sfpersona.SiEstado = 1;

                            //SF CreatePatient

                            var param = new
                            {
                                Cesado__c = int.Parse(sfpersona.Cesado),
                                Tipo_de_documento__c = sfpersona.TipoDocumento,
                                Documento_identidad__c = sfpersona.DocumentoIdentidad,
                                Fecha_cesado__c = string.IsNullOrEmpty(sfpersona.FechaCesado) ? (string?)null : DateTime.ParseExact(sfpersona.FechaCesado, "dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd"),
                                //Fecha_de_evaluacion__c = sfpersona.EXAM_FECHA_INICIO?.ToString("yyyy-MM-dd"),
                                FirstName = sfpersona.PrimerNombre,
                                MiddleName = sfpersona.SegundoNombre,
                                LastName = sfpersona.ApellidoPaterno,
                                Segundo_apellido__c = sfpersona.ApellidoMaterno,
                                Grado_de_instruccion__c = sfpersona.GradoInstruccion,
                                HealthCloudGA__Gender__pc = sfpersona.Sexo,
                                PersonBirthdate = string.IsNullOrEmpty(sfpersona.FechaNacimiento) ? "" : DateTime.ParseExact(sfpersona.FechaNacimiento, "dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd"),
                                PersonEmail = sfpersona.Correo,
                                PersonHomePhone = sfpersona.Celular,
                                PersonMaritalStatus = sfpersona.EstadoCivil,
                                PersonMobilePhone = sfpersona.Celular,
                                RecordType = new
                                {
                                    Name = "Cuenta personal"
                                },

                                Validado__c = int.Parse(sfpersona.Validado)
                            };

                            string url = $"{ServiceUrl}{@_configuration["SalesForce:SrvAcount"]}";
                            string method = "POST";
                            string contentType = "application/json; charset=utf-8";
                            string accessToken = AuthToken;

                            _logger.LogInformation("Salesforce - Account ALTA");

                            string body = JsonConvert.SerializeObject(param);
                            string bodylog = JsonConvert.SerializeObject(param, Newtonsoft.Json.Formatting.Indented);
                            

                            _logger.LogInformation(bodylog);

                            byte[] result = Helper.executeRequestSalesForce(url: url
                            , method: method
                            , contentType: contentType
                            , accessToken: accessToken
                            , Encoding.UTF8.GetBytes(body));

                            if (result != null && result.Length > 0)
                            {
                                var resstr = Encoding.UTF8.GetString(result);
                                try
                                {
                                    JObject ressf = JObject.Parse(resstr);
                                    bool bsuccess = bool.Parse(ressf["success"]?.ToString());
                                    IdPaciente = ressf["id"]?.ToString();
                                    //IdPaciente = idpaciente;
                                    if (bsuccess)
                                    {
                                        sfpersona.IdPaciente = IdPaciente;
                                    }
                                    else
                                    {
                                        throw new Exception(resstr);
                                    }
                                }
                                catch (Exception ex)
                                {
                                    throw new Exception(resstr);
                                }
                            }
                            else
                            {
                                throw new Exception("Error al conectarse a SF");
                            }
                            //END SF CreatePatient




                            await _bnv.SF_InsertPacienteAlta(sfpersona);
                        }
                        catch (Exception ex)
                        {
                            _logger.LogError(ex.Message + $", DNI: {dni}");
                            //await _bnv.SF_InsertError(ent.DNI, ent.IdEmpresa, ex.Message);
                        }
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex.Message);
                    //await _bnv.SF_InsertError(ent.DNI, ent.IdEmpresa, ex.Message);
                }
                _logger.LogInformation("-----> SF_EnviasDatosAltaPaciente fin");
            }
            return true;
        }

        public async Task<object> SF_EnviasDatosMedicos()
        {
            if (procesarSF)
            {
                _logger.LogInformation("-----> SF_EnviasDatosMedicos inicio");

                //TODOS
                try
                {
                    //SF Init
                    string LoginEndpoint = @_configuration["SalesForce:LoginEndpoint"];

                    string Username = @_configuration["SalesForce:Username"];
                    string Password = @_configuration["SalesForce:Password"];
                    string ClientId = @_configuration["SalesForce:ClientId"];
                    string ClientSecret = @_configuration["SalesForce:ClientSecret"];
                    string AuthToken = "";
                    string Token = "";
                    string ServiceUrl = "";

                    HttpClient Client;
                    Client = new HttpClient();

                    HttpContent content = new FormUrlEncodedContent(new Dictionary<string, string>
                                  {
                                      {"grant_type", "password"},
                                      {"client_id", ClientId},
                                      {"client_secret", ClientSecret},
                                      {"username", Username},
                                      {"password", Password}
                                  });

                    HttpResponseMessage message = Client.PostAsync(LoginEndpoint, content).Result;

                    string response = message.Content.ReadAsStringAsync().Result;
                    JObject obj = JObject.Parse(response);

                    AuthToken = (string)obj["access_token"];
                    ServiceUrl = (string)obj["instance_url"];
                    //END SF Init

                    var pacientes = await _bnv.SF_GetPacientesPendientesDeEnviarDatos();
                    foreach (var pac in pacientes)
                    {
                        try
                        {
                            var datos = await _bnv.SF_GetPacienteDatosMedicosIni(pac.VcNumeroIdentificacion);

                            var cod_episodio = datos.First(x => x.Dato_Campo_Codigo == "Ruc_Compania").EXAM_COD_ATENCION;


                            var IdExamen = await _bnv.SF_GetPacienteExamenExiste(pac.Id_paciente, cod_episodio);

                            _logger.LogInformation($"Salesforce - Examen, idpaciente {pac.Id_paciente}, idexamen {IdExamen} , episodio {cod_episodio}");

                            if (string.IsNullOrEmpty(IdExamen)) //enviar datos a SF
                            {

                                var listDocument = new List<object>();
                                var listDatosMedicos = new List<object>();

                                //titular
                                listDocument.Add(new
                                {
                                    method = "PATCH",
                                    url = $"/services/data/v56.0/sobjects/Account/RUC_compania_cuenta__c/{datos.First(x => x.Dato_Campo_Codigo == "Ruc_Compania").Dato}",
                                    referenceId = "refAccount",
                                    body = new
                                    {
                                        Name = datos.First(x => x.Dato_Campo_Codigo == "Des_Compania").Dato,
                                        Actividad_Economica_CIIU__c = datos.First(x => x.Dato_Campo_Codigo == "ActividadEconomicaCIUU").Dato.Equals("-") ? (string?)null : datos.First(x => x.Dato_Campo_Codigo == "ActividadEconomicaCIUU").Dato,
                                        Actividad_Economica_Descripcion__c = datos.First(x => x.Dato_Campo_Codigo == "ActividadEconomicaDescripcion").Dato.Equals("-") ? (string?)null : datos.First(x => x.Dato_Campo_Codigo == "ActividadEconomicaDescripcion").Dato,
                                        RecordType = new
                                        {
                                            Name = "Business Account"
                                        }
                                    }
                                });

                                //puesto
                                var puesto = datos.First(x => x.Dato_Campo_Codigo == "Puesto").Dato.Equals("-") ? (string?)null : datos.First(x => x.Dato_Campo_Codigo == "Puesto").Dato;

                                listDocument.Add(new
                                {
                                    method = "PATCH",
                                    url = "/services/data/v56.0/sobjects/Puesto_de_trabajo__c/PacienteId__c/@{refAccount.id}-" + pac.Id_paciente + $"-{puesto}",
                                    referenceId = "refPuesto",
                                    body = new
                                    {
                                        Name = puesto,
                                        RUC_compania__c = datos.First(x => x.Dato_Campo_Codigo == "Ruc_Compania").Dato,
                                        Compania__c = "@{refAccount.id}",
                                        RUC_de_la_contrata__c = datos.First(x => x.Dato_Campo_Codigo == "Ruc_Contrata").Dato,
                                        Descripcion_de_la_contrata__c = datos.First(x => x.Dato_Campo_Codigo == "Des_Contrata").Dato,
                                        Codigo_de_la_unidad__c = datos.First(x => x.Dato_Campo_Codigo == "Id_Unidad").Dato,
                                        Unidad__c = datos.First(x => x.Dato_Campo_Codigo == "Des_Unidad").Dato,
                                        Altitud_geografica__c = datos.First(x => x.Dato_Campo_Codigo == "AltitudGeografica").Dato,
                                        Zona__c = datos.First(x => x.Dato_Campo_Codigo == "Zona").Dato,
                                        Area__c = datos.First(x => x.Dato_Campo_Codigo == "Area").Dato,
                                        Account__c = pac.Id_paciente
                                    }
                                });

                                //examen
                                listDocument.Add(new
                                {
                                    method = "POST",
                                    url = "/services/data/v56.0/sobjects/CareObservation",
                                    referenceId = "refCareObservation",
                                    body = new
                                    {
                                        Name = datos.First(x => x.Dato_Campo_Codigo == "TipoExamen").Dato,
                                        ObservationStatus = "Registrado",
                                        Tipo_de_examen__c = datos.First(x => x.Dato_Campo_Codigo == "TipoExamen").Dato,
                                        ObservationStartTime = $"{DateTime.ParseExact(datos.First(x => x.Dato_Campo_Codigo == "FechaExamen").Dato, "dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd")}T00:00:00-05:00",
                                        ObservationEndTime = $"{DateTime.ParseExact(datos.First(x => x.Dato_Campo_Codigo == "FechaExamen").Dato, "dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd")}T00:00:00-05:00",
                                        ObservedSubjectId = pac.Id_paciente,
                                        Code = new
                                        {
                                            CodeSetKey = datos.First(x => x.Dato_Campo_Codigo == "TipoExamen").Dato
                                        }
                                    }
                                });

                                //datos médicos

                                var _datosmedicos = new List<SFDatosMedicos>();
                                _datosmedicos = datos.Where(x => x.Dato_Tipo_Clasificacion == false).ToList();

                                foreach (var item in _datosmedicos)
                                {
                                    listDatosMedicos.Add(new
                                    {
                                        attributes = new
                                        {
                                            type = "CareObservationComponent"
                                        },
                                        Name = item.Campo,
                                        NumericValue = item.Dato_Campo_Tipo.Equals("Numero") && !item.Dato.Equals("-") && !item.Dato.Equals("") ? float.Parse(item.Dato) : (float?)null,
                                        ObservedValueText = item.Dato_Campo_Tipo.Equals("Texto") && !item.Dato.Equals("-") ? item.Dato : (string?)null,
                                        ObservationStartDateTime = item.Dato_Campo_Tipo.Equals("Fecha") && !item.Dato.Equals("-") && !item.Dato.Equals("") ? DateTime.ParseExact(item.Dato, "dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd") + "T00:00:00-05:00" : (string?)null,
                                        SI_NO__c = item.Dato_Campo_Tipo.Equals("Booleano") && !item.Dato.Equals("-") && !item.Dato.Equals("") ? short.Parse(item.Dato) : (short)0,
                                        CareObservationId = "@{refCareObservation.id}",
                                        ComponentTypeCode = new
                                        {
                                            CodeSetBundleKey = item.Dato_Campo_Codigo
                                        },
                                        ExamenId__c = "@{refCareObservation.id}-" + item.Dato_Campo_Codigo
                                    });
                                }

                                listDocument.Add(new
                                {
                                    method = "POST",
                                    url = "/services/data/v56.0/composite/sobjects",
                                    referenceId = "refCareObservationComp",
                                    body = new
                                    {
                                        allOrNone = false,
                                        records = listDatosMedicos
                                    }
                                });

                                var param = new
                                {
                                    allOrNone = "true",
                                    compositeRequest = listDocument
                                };

                                string url = $"{ServiceUrl}/services/data/v56.0/composite/";
                                string method = "POST";
                                string contentType = "application/json; charset=utf-8";
                                string accessToken = AuthToken;

                                _logger.LogInformation("Salesforce - Examen");

                                string body = JsonConvert.SerializeObject(param);
                                string bodylog = JsonConvert.SerializeObject(param, Newtonsoft.Json.Formatting.Indented);

                                _logger.LogInformation(bodylog);

                                byte[] result = Helper.executeRequestSalesForce(url: url
                                , method: method
                                , contentType: contentType
                                , accessToken: accessToken
                                , Encoding.UTF8.GetBytes(body));

                                if (result != null && result.Length > 0)
                                {
                                    var resstr = Encoding.UTF8.GetString(result);
                                    try
                                    {
                                        JObject ressf = JObject.Parse(resstr);
                                        IdExamen = (ressf.First.First[2]).First.First["id"].ToString();

                                        //Inserta SF_EXAMEN
                                        await _bnv.SF_ExamensPacienteAlta(IdExamen, datos.First(x => x.Dato_Campo_Codigo == "Ruc_Compania").Dato
                                            , pac.Id_paciente, cod_episodio, datos.First(x => x.Dato_Campo_Codigo == "FechaExamen").Dato,
                                            datos.First(x => x.Dato_Campo_Codigo == "TipoExamen").Dato,
                                            datos.First(x => x.Dato_Campo_Codigo == "Ruc_Contrata").Dato,
                                            datos.First(x => x.Dato_Campo_Codigo == "Id_Unidad").Dato,
                                            datos.First(x => x.Dato_Campo_Codigo == "AltitudGeografica").Dato.Replace(",", ".").Replace("-", "0"),
                                            datos.First(x => x.Dato_Campo_Codigo == "Zona").Dato,
                                            datos.First(x => x.Dato_Campo_Codigo == "Area").Dato
                                            );

                                    }
                                    catch (Exception ex)
                                    {
                                        throw new Exception(resstr);
                                    }
                                }
                                else
                                {
                                    throw new Exception($"Error al grabar examen en SF id paciente {pac.Id_paciente}");
                                }


                            }
                        }
                        catch (Exception ex)
                        {
                            _logger.LogError(ex.Message + $", DNI: {pac.VcNumeroIdentificacion}");
                            //await _bnv.SF_InsertError(ent.DNI, ent.IdEmpresa, ex.Message);
                        }
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex.Message);
                    //await _bnv.SF_InsertError(ent.DNI, ent.IdEmpresa, ex.Message);
                }
                _logger.LogInformation("-----> SF_EnviasDatosMedicos fin");
            }
            return true;
        }

        public async Task<object> GenerarParquet()
        {
            _logger.LogInformation("-----> GenerarBNV inicio");

            //TODOS
            try
            {
                var xmlstrTodos = SOAP_BNV.TrabajadoresTodos();
                XmlDocument docTodos = new XmlDocument();
                docTodos.LoadXml(xmlstrTodos);
                int z = 0;
                foreach (XmlNode node in docTodos.DocumentElement.ChildNodes[0].ChildNodes[0].ChildNodes[0].ChildNodes[1].ChildNodes[0].ChildNodes)
                {
                    try
                    {
                        XmlDocument docinner = new XmlDocument();
                        docinner.LoadXml(@"<trabajador>" + node.InnerXml + "</trabajador>");

                        BNVTrab ent = new BNVTrab();
                        ent.CodigoTrabajador = docinner.DocumentElement.ChildNodes[0]?.InnerText;
                        ent.IdEmpleado = docinner.DocumentElement.ChildNodes[1]?.InnerText;
                        ent.FechaAlta = docinner.DocumentElement.ChildNodes[2]?.InnerText;
                        ent.FechaCese = docinner.DocumentElement.ChildNodes[3]?.InnerText;
                        ent.ApellidoPaterno = docinner.DocumentElement.ChildNodes[4]?.InnerText;
                        ent.ApellidoMaterno = docinner.DocumentElement.ChildNodes[5]?.InnerText;
                        ent.Nombres = docinner.DocumentElement.ChildNodes[6]?.InnerText;
                        ent.Sexo = docinner.DocumentElement.ChildNodes[7]?.InnerText;
                        ent.EstadoCivil = docinner.DocumentElement.ChildNodes[8]?.InnerText;
                        ent.Edad = docinner.DocumentElement.ChildNodes[9]?.InnerText;
                        ent.FechaNacimiento = docinner.DocumentElement.ChildNodes[10]?.InnerText;
                        ent.TipoDocumento = docinner.DocumentElement.ChildNodes[11]?.InnerText;
                        ent.DNI = docinner.DocumentElement.ChildNodes[12]?.InnerText;
                        ent.IdEmpresa = docinner.DocumentElement.ChildNodes[13]?.InnerText;
                        ent.IdTipoEmpleado = docinner.DocumentElement.ChildNodes[14]?.InnerText;
                        ent.IdPuesto = docinner.DocumentElement.ChildNodes[15]?.InnerText;
                        ent.NombrePuesto = docinner.DocumentElement.ChildNodes[16]?.InnerText;
                        ent.IdArea = docinner.DocumentElement.ChildNodes[17]?.InnerText;
                        ent.NombreArea = docinner.DocumentElement.ChildNodes[18]?.InnerText;
                        ent.IdContrata = docinner.DocumentElement.ChildNodes[19]?.InnerText;
                        ent.NombreContrata = docinner.DocumentElement.ChildNodes[20]?.InnerText;
                        ent.FechaInicioContrata = docinner.DocumentElement.ChildNodes[21]?.InnerText;
                        ent.FechaFinContrata = docinner.DocumentElement.ChildNodes[22]?.InnerText;
                        ent.CodigoTipoContrato = docinner.DocumentElement.ChildNodes[23]?.InnerText;
                        ent.TipoContrato = docinner.DocumentElement.ChildNodes[24]?.InnerText;
                        ent.Direccion = docinner.DocumentElement.ChildNodes[25]?.InnerText;
                        ent.Telefono = docinner.DocumentElement.ChildNodes[26]?.InnerText;
                        ent.Email = docinner.DocumentElement.ChildNodes[27]?.InnerText;
                        ent.UbigeoNacimiento = docinner.DocumentElement.ChildNodes[28]?.InnerText;
                        ent.UbigeoDomicilio = docinner.DocumentElement.ChildNodes[29]?.InnerText;
                        ent.NivelDeRiesgo = docinner.DocumentElement.ChildNodes[30]?.InnerText;
                        ent.Celula = docinner.DocumentElement.ChildNodes[31]?.InnerText;
                        ent.Anillo = docinner.DocumentElement.ChildNodes[32]?.InnerText;
                        ent.Categoria = docinner.DocumentElement.ChildNodes[33]?.InnerText;
                        ent.RestriccionesVigentes = docinner.DocumentElement.ChildNodes[34]?.InnerText;
                        ent.UltimaMarcaDeAcceso = docinner.DocumentElement.ChildNodes[35]?.InnerText;
                        ent.UltimaAreaDeAcceso = docinner.DocumentElement.ChildNodes[36]?.InnerText;
                        ent.EntradaSalidaPrincipal = docinner.DocumentElement.ChildNodes[37]?.InnerText;
                        ent.TipoPerfil = docinner.DocumentElement.ChildNodes[38]?.InnerText;
                        ent.CodigoDeLaLocalidad = docinner.DocumentElement.ChildNodes[39]?.InnerText;
                        ent.DescripcionDeLaLocalidad = docinner.DocumentElement.ChildNodes[40]?.InnerText;
                        ent.FechaUltimaModificacion = docinner.DocumentElement.ChildNodes[41]?.InnerText;
                        ent.ContinuaLaborando = docinner.DocumentElement.ChildNodes[42]?.InnerText;
                        //Console.WriteLine($"{CodigoTrabajador} {IdEmpleado} {FechaAlta} {FechaCese} {EstadoCivil}  {ApellidoPaterno}  {ApellidoMaterno}  {Nombres}  {Sexo}");
                        //z++;
                        await _bnv.RegistraTrabajador(ent);
                    }
                    catch (Exception ex1) { _logger.LogError(ex1.Message); }
                }

                await _bnv.CorrijeBajas();

            }
            catch (Exception ex) { _logger.LogError(ex.Message); }

            _logger.LogInformation("-----> GenerarBNV fin");

            return true;
        }




    }
}
