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
using Newtonsoft.Json;
using Nancy.Json;
using System.Net.Http;
using System.Text;

namespace ACME.WorkerService.Tasks
{
    internal interface ISAPAsientosTasks
    {
        Task<object> GenerarAsientosPendientes();
    }

    internal class SAPAsientosTasks : ISAPAsientosTasks
    {
        private readonly ISUNATPagoManager _sunat;
        private readonly IConfiguration _configuration;
        private readonly ILogger _logger;
        private readonly ICloudStorage _cloudStorage;
        private readonly IPDFService _pdfservice;
        private readonly string _logoPath;
        private readonly string _urlApiDocumento;
        private readonly int iIdusuario;

        public SAPAsientosTasks(IPDFService pdfservice, ISUNATPagoManager sunat, ILogger<SAPAsientosTasks> logger, IConfiguration configuration, ICloudStorage cloudStorage)
        {
            _pdfservice = pdfservice;
            _sunat = sunat;
            _configuration = configuration;
            _logger = logger;
            _cloudStorage = cloudStorage;
            _logoPath = @_configuration["LogoPath"];
            _urlApiDocumento = @_configuration["WORKER_SAPAsientoServices:UrlSAP"];
            iIdusuario = int.Parse(_configuration["WORKER_SAPAsientoServices:IdUsuario"]);
            InitTasks();
        }

        private void InitTasks()
        {


        }



        //cambio tmp por
        public async Task<object> GenerarAsientosPendientes()
        {
            try
            {
                _logger.LogInformation("-----> Asientos Contado Comprobante Ini");
                var asientoscontadopend = await _sunat.getSAPAsientosContadoPend();
                foreach (var contadopen in asientoscontadopend)
                {
                    try
                    {
                        var contadocab = await _sunat.getSAPAsientosContadoCab((int)contadopen.IIdCitaSunatCab, contadopen.VcTipoDoc);
                        var contadodet = await _sunat.getSAPAsientosContadoDet((int)contadopen.IIdCitaSunatCab);

                        var requestupdate = new TCI_SAP(this).GeneraAsientoContado_SAP(contadocab, contadodet, "");

                        _logger.Log(LogLevel.Information, $"SAP RES Asiento Contado: {requestupdate}");

                        dynamic dataupdate = new JavaScriptSerializer().DeserializeObject(requestupdate);
                        var CODUPDE = dataupdate?.Response?.code;


                        if (CODUPDE < 0)
                        {
                            var msg = dataupdate?.Response?.message?.value;
                            throw new Exception(msg);
                        }

                        var DocEntryres = dataupdate?.Response?.message?.value?.TransId;
                        _logger.Log(LogLevel.Information, $"SAP RES Asiento Contado TransId: {DocEntryres}");

                        var requestupdateupd = new TCI_SAP(this).ActualizaAsientoContado_SAP(DocEntryres, contadopen.U_VKP_TipoDoc, contadopen.DocEntry);
                        _logger.Log(LogLevel.Information, $"SAP RES Asiento Contado update: {requestupdateupd}");

                        await _sunat.updateSAPAsientosContado((int)contadopen.IIdCitaSunatCab, contadopen.DocEntry, DocEntryres);
                    }
                    catch (Exception ex1)
                    {
                        _logger.LogError("Asientos Contado Comprobante" + contadopen?.IIdCitaSunatCab?.ToString() +" --> "+ ex1.Message);
                    }
                }

                _logger.LogInformation("-----> Asientos Contado Comprobante Fin");

                _logger.LogInformation("-----> Asientos Contado NC Ini");

                var asientoscontadopendNC = await _sunat.getSAPAsientosContadoPend_NC();
                foreach (var contadopen in asientoscontadopendNC)
                {
                    try
                    {
                        var contadocab = await _sunat.getSAPAsientosContadoCab_NC((int)contadopen.IIdCitaSunatCab, contadopen.IIdCitaSunatDet, (bool)contadopen.BCabecera);
                        var contadodet = await _sunat.getSAPAsientosContadoDet_NC((int)contadopen.IIdCitaSunatCab, contadopen.IIdCitaSunatDet, (bool)contadopen.BCabecera);

                        var requestupdate = new TCI_SAP(this).GeneraAsientoContado_SAP(contadocab, contadodet, "");

                        _logger.Log(LogLevel.Information, $"SAP RES Asiento Contado NC: {requestupdate}");

                        dynamic dataupdate = new JavaScriptSerializer().DeserializeObject(requestupdate);
                        var CODUPDE = dataupdate?.Response?.code;


                        if (CODUPDE < 0)
                        {
                            var msg = dataupdate?.Response?.message?.value;
                            throw new Exception(msg);
                        }

                        var DocEntryres = dataupdate?.Response?.message?.value?.TransId;
                        _logger.Log(LogLevel.Information, $"SAP RES Asiento Contado  NC TransId: {DocEntryres}");

                        var requestupdateupd = new TCI_SAP(this).ActualizaAsientoContado_SAP(DocEntryres, contadopen.U_VKP_TipoDoc, contadopen.DocEntry);
                        _logger.Log(LogLevel.Information, $"SAP RES Asiento Contado NC update: {requestupdateupd}");

                        await _sunat.updateSAPAsientosContado_NC((int)contadopen.IIdCitaSunatCab, contadopen.IIdCitaSunatDet, (bool)contadopen.BCabecera, contadopen.DocEntry, DocEntryres);
                    }
                    catch (Exception ex1)
                    {
                        _logger.LogError("Asientos Contado NC " + contadopen?.IIdCitaSunatCab?.ToString() + " --> " + ex1.Message);
                    }
                }
                _logger.LogInformation("-----> Asientos Contado NC Fin");

                _logger.LogInformation("-----> Asientos Crédito Ini");

                var asientoscreditopend = await _sunat.getSAPAsientosCreditoPend();
                foreach (var contadopen in asientoscreditopend)
                {
                    try
                    {
                        var contadocab = await _sunat.getSAPAsientosCreditoCab((long)contadopen.IIdCitaSunatCab);
                        var contadodet = await _sunat.getSAPAsientosCreditoDet((long)contadopen.IIdCitaSunatCab);

                        var requestupdate = new TCI_SAP(this).GeneraAsientoContado_SAP(contadocab, contadodet, "");

                        _logger.Log(LogLevel.Information, $"SAP RES Asiento Crédito: {requestupdate}");

                        dynamic dataupdate = new JavaScriptSerializer().DeserializeObject(requestupdate);
                        var CODUPDE = dataupdate?.Response?.code;


                        if (CODUPDE < 0)
                        {
                            var msg = dataupdate?.Response?.message?.value;
                            throw new Exception(msg);
                        }

                        var DocEntryres = dataupdate?.Response?.message?.value?.TransId;
                        _logger.Log(LogLevel.Information, $"SAP RES Asiento Crédito TransId: {DocEntryres}");

                        var requestupdateupd = new TCI_SAP(this).ActualizaAsientoContado_SAP(DocEntryres, contadopen.U_VKP_TipoDoc, contadopen.DocEntry);
                        _logger.Log(LogLevel.Information, $"SAP RES Asiento Crédito update: {requestupdateupd}");

                        await _sunat.updateSAPAsientosCredito(contadopen.DocEntry, DocEntryres);
                    }
                    catch (Exception ex1)
                    {
                        _logger.LogError("Asientos Crédito IIdCitaSunatCab: " + contadopen?.IIdCitaSunatCab?.ToString() + " --> " + ex1.Message);
                    }
                }
                _logger.LogInformation("-----> Asientos Crédito Fin");


                _logger.LogInformation("-----> Asientos Crédito ND Ini");

                var asientoscredito_nd_pend = await _sunat.getSAPAsientosCredito_ND_Pend();
                foreach (var contadopen in asientoscredito_nd_pend)
                {
                    try
                    {
                        var contadocab = await _sunat.getSAPAsientosCredito_ND_Cab((long)contadopen.IIdCitaSunatCab, contadopen.DocEntry);
                        var contadodet = await _sunat.getSAPAsientosCredito_ND_Det((long)contadopen.IIdCitaSunatCab, contadopen.DocEntry);

                        var requestupdate = new TCI_SAP(this).GeneraAsientoContado_SAP(contadocab, contadodet, "");

                        _logger.Log(LogLevel.Information, $"SAP RES Asiento Crédito ND: {requestupdate}");

                        dynamic dataupdate = new JavaScriptSerializer().DeserializeObject(requestupdate);
                        var CODUPDE = dataupdate?.Response?.code;


                        if (CODUPDE < 0)
                        {
                            var msg = dataupdate?.Response?.message?.value;
                            throw new Exception(msg);
                        }

                        var DocEntryres = dataupdate?.Response?.message?.value?.TransId;
                        _logger.Log(LogLevel.Information, $"SAP RES Asiento Crédito ND TransId: {DocEntryres}");

                        var requestupdateupd = new TCI_SAP(this).ActualizaAsientoContado_SAP(DocEntryres, contadopen.U_VKP_TipoDoc, contadopen.DocEntry);
                        _logger.Log(LogLevel.Information, $"SAP RES Asiento Crédito ND update: {requestupdateupd}");

                        await _sunat.updateSAPAsientosCredito(contadopen.DocEntry, DocEntryres);
                    }
                    catch (Exception ex1)
                    {
                        _logger.LogError("Asientos Crédito ND DocEntry:" + contadopen?.DocEntry?.ToString() + " --> " + ex1.Message);
                    }
                }
                _logger.LogInformation("-----> Asientos Crédito ND Fin");


                _logger.LogInformation("-----> Asientos Crédito NC Ini");

                var asientoscredito_nc_pend = await _sunat.getSAPAsientosCredito_NC_Pend();
                foreach (var contadopen in asientoscredito_nc_pend)
                {
                    try
                    {
                        var contadocab = await _sunat.getSAPAsientosCredito_NC_Cab((long)contadopen.IIdCitaSunatCab, contadopen.DocEntry);
                        var contadodet = await _sunat.getSAPAsientosCredito_NC_Det((long)contadopen.IIdCitaSunatCab, contadopen.DocEntry);

                        var requestupdate = new TCI_SAP(this).GeneraAsientoContado_SAP(contadocab, contadodet, "");

                        _logger.Log(LogLevel.Information, $"SAP RES Asiento Crédito NC: {requestupdate}");

                        dynamic dataupdate = new JavaScriptSerializer().DeserializeObject(requestupdate);
                        var CODUPDE = dataupdate?.Response?.code;


                        if (CODUPDE < 0)
                        {
                            var msg = dataupdate?.Response?.message?.value;
                            throw new Exception(msg);
                        }

                        var DocEntryres = dataupdate?.Response?.message?.value?.TransId;
                        _logger.Log(LogLevel.Information, $"SAP RES Asiento Crédito NC TransId: {DocEntryres}");

                        var requestupdateupd = new TCI_SAP(this).ActualizaAsientoContado_SAP(DocEntryres, contadopen.U_VKP_TipoDoc, contadopen.DocEntry);
                        _logger.Log(LogLevel.Information, $"SAP RES Asiento Crédito NC update: {requestupdateupd}");

                        await _sunat.updateSAPAsientosCredito(contadopen.DocEntry, DocEntryres);
                    }
                    catch (Exception ex1)
                    {
                        _logger.LogError("Asientos Crédito NC DocEntry: " + contadopen?.DocEntry?.ToString() + " --> " + ex1.Message);
                    }
                }
                _logger.LogInformation("-----> Asientos Crédito NC Fin");

                _logger.LogInformation("-----> Asientos Baja Ini");

                var asientos_bajas_pend = await _sunat.getSAPAsientosaja_Pend();
                foreach (var contadopen in asientos_bajas_pend)
                {
                    try
                    {
                        var requestupdate = new TCI_SAP(this).BajaAsientoSAP(contadopen.TransId);
                         if (requestupdate.statusCode != HttpStatusCode.OK)
                        {
                            throw new Exception(requestupdate.resultContent);
                        }
                        var resultContent = requestupdate.resultContent;
                        _logger.Log(LogLevel.Information, $"SAP RES Asiento Baja: {resultContent}");
                        await _sunat.updateSAPAsientosBaja(contadopen.TransId);
                    }
                    catch (Exception ex1)
                    {
                        _logger.LogError("Asientos Baja TransId: " + contadopen?.TransId?.ToString() + " --> " + ex1.Message);
                    }
                }
                _logger.LogInformation("-----> Asientos Baja Fin");
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return false;
            }
        }

        #region TCI_SAP
        internal class TCI_SAP
        {
            private string URL = "";
            private SAPAsientosTasks _manager;

            public TCI_SAP(SAPAsientosTasks manager)
            {
                this._manager = manager;
            }


            public (HttpStatusCode statusCode, string resultContent) BajaAsientoSAP(string TransId)
            {

                URL = $"{_manager._urlApiDocumento}/financials/cancelJournalEntry.xsjs?id={TransId}";
                return new Util().Request(null, URL, null, "get");
            }
            //asiento ACTUALIZA asientos
            public string ActualizaAsientoContado_SAP(string U_VKP_TransId,string U_VKP_TipoDoc, string id)
            {

                URL = $"{_manager._urlApiDocumento}/financials/updateComprobante.xsjs";

                var AdmInfo = new
                {
                    Object = "30",
                    Version = "1"
                };

                var Document = new
                {
                    row = new
                    {
                        U_VKP_TransId,
                        U_VKP_TipoDoc,
                        DocEntry = id

                    }
                };

                

                var body = new
                {
                    BOM = new
                    {
                        BO = new
                        {
                            AdmInfo,
                            Document
                        }
                    }

                };

                _manager._logger.Log(LogLevel.Information, $"SAP Asiento actualiza Docentry: {id}");
                _manager._logger.Log(LogLevel.Information, $"SAP URL: {URL}");
                var jbody = JsonConvert.SerializeObject(body);

                _manager._logger.Log(LogLevel.Information, $"SAP Asiento actualiza Docentry : {jbody}");

                SAP sap = new SAP();
                return sap.BPUPdate(URL, jbody, id);
            }
            

            //asiento contado
            public string GeneraAsientoContado_SAP(SAPAsientoContadoCab cab, IEnumerable<SAPAsientoContadoDet> det, string id)
            {

                URL = $"{_manager._urlApiDocumento}/financials/addJournalEntry.xsjs";

                var AdmInfo = new
                {
                    Object = "30",
                    Version = "1"
                };

                var Document = new
                {
                    row = new
                    {
                        TransCode = cab.TransCode,
                        DocCur = cab.DocCur,
                        DocDate = cab.DocDate,
                        DocDueDate = cab.DocDueDate,
                        TaxDate = cab.TaxDate,
                        Ref1 = cab.Ref1,
                        Ref2 = cab.Ref2,
                        Memo = cab.Memo

                    }
                };

                var listDocument = new List<object>();

                foreach (var item in det)
                {
                    listDocument.Add(new
                    {
                        Account = item.Account,
                        FCDebit = item.FCDebit,
                        Debit = item.Debit,
                        FCCredit = item.FCCredit,
                        Credit = item.Credit,
                        OcrCode2 = item.OcrCode2,
                        OcrCode3 = item.OcrCode3,
                        OcrCode4 = item.OcrCode4,
                        OcrCode5 = item.OcrCode5
                    });
                }

                var Document_Lines = new
                {
                    row = listDocument
                };

                var body = new
                {
                    BOM = new
                    {
                        BO = new
                        {
                            AdmInfo,
                            Document,
                            JournalEntry_Lines = Document_Lines
                        }
                    }

                };

                _manager._logger.Log(LogLevel.Information, $"SAP Asiento: {cab.Ref1}");
                _manager._logger.Log(LogLevel.Information, $"SAP URL: {URL}");
                var jbody = JsonConvert.SerializeObject(body);

                _manager._logger.Log(LogLevel.Information, $"SAP asiento {cab.Ref1}: {jbody}");

                SAP sap = new SAP();
                return sap.BPUPdate(URL, jbody, id);
            }
            //END asiento contado

        }
        #endregion TCI_SAP
        internal class Util
        {
            //private readonly ILogger<Util> _logger;
            internal Util() { }

            public (HttpStatusCode statusCode, string resultContent) Request(object data, string url, string api_key, string type_method)
            {
                HttpResponseMessage result = null;
                using var client = new HttpClient();
                //client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", api_key);
                if (!string.IsNullOrEmpty(api_key))
                {
                    client.DefaultRequestHeaders.Add("X-Auth-Token", api_key);
                }
                //client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                if (type_method.Equals("get"))
                {
                    result = client.GetAsync(url).Result;
                }
                else if (type_method.Equals("post"))
                {
                    var json = JsonConvert.SerializeObject(data);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");
                    result = client.PostAsync(url, content).Result;
                }
                else if (type_method.Equals("put"))
                {
                    var json = JsonConvert.SerializeObject(data);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");
                    result = client.PutAsync(url, content).Result;
                }
                else if (type_method.Equals("delete"))
                {
                    result = client.DeleteAsync(url).Result;
                }

                if (result.StatusCode != HttpStatusCode.Created)
                {

                }

                string resultContent = result.Content.ReadAsStringAsync().Result;

                return (result.StatusCode, resultContent);
            }
        }

    }
}
