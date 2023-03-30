using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Net;
using System.IO;

namespace ACME.WorkerService.Helpers
{
    public static class SOAP_BNV
    {
        private static string _url = "***.asmx";
        private static string _url_todos = "***.asmx";
        private static string _action_inactivos = "***";
        private static string _action_activos = "***";
        private static string _action_todos = "http://tempuri.org/ConsultarTrabajadores";
        public static string TrabajadoresInactivos()
        {

            var ret = "";
            XmlDocument soapEnvelopeXml = CreateSoapInactivosEnvelope();
            HttpWebRequest webRequest = CreateWebRequest(_url, _action_inactivos);
            InsertSoapEnvelopeIntoWebRequest(soapEnvelopeXml, webRequest);

            // begin async call to web request.
            IAsyncResult asyncResult = webRequest.BeginGetResponse(null, null);

            // suspend this thread until call is complete. You might want to
            // do something usefull here like update your UI.
            asyncResult.AsyncWaitHandle.WaitOne();

            // get the response from the completed web request.
            string soapResult;
            using (WebResponse webResponse = webRequest.EndGetResponse(asyncResult))
            {
                using (StreamReader rd = new StreamReader(webResponse.GetResponseStream()))
                {
                    soapResult = rd.ReadToEnd();
                }
                ret = soapResult;
            }
            return ret;
        }

        public static string TrabajadoresTodos()
        {

            var ret = "";
            XmlDocument soapEnvelopeXml = CreateSoapTodosEnvelope();
            HttpWebRequest webRequest = CreateWebRequest(_url_todos, _action_todos);
            InsertSoapEnvelopeIntoWebRequest(soapEnvelopeXml, webRequest);

            // begin async call to web request.
            IAsyncResult asyncResult = webRequest.BeginGetResponse(null, null);

            // suspend this thread until call is complete. You might want to
            // do something usefull here like update your UI.
            asyncResult.AsyncWaitHandle.WaitOne();

            // get the response from the completed web request.
            string soapResult;
            using (WebResponse webResponse = webRequest.EndGetResponse(asyncResult))
            {
                using (StreamReader rd = new StreamReader(webResponse.GetResponseStream()))
                {
                    soapResult = rd.ReadToEnd();
                }
                ret = soapResult;
            }
            return ret;
        }
        public static string TrabajadoresActivos()
        {

            var ret = "";
            XmlDocument soapEnvelopeXml = CreateSoapActivosEnvelope();
            HttpWebRequest webRequest = CreateWebRequest(_url, _action_activos);
            InsertSoapEnvelopeIntoWebRequest(soapEnvelopeXml, webRequest);

            // begin async call to web request.
            IAsyncResult asyncResult = webRequest.BeginGetResponse(null, null);

            // suspend this thread until call is complete. You might want to
            // do something usefull here like update your UI.
            asyncResult.AsyncWaitHandle.WaitOne();

            // get the response from the completed web request.
            string soapResult;
            using (WebResponse webResponse = webRequest.EndGetResponse(asyncResult))
            {
                using (StreamReader rd = new StreamReader(webResponse.GetResponseStream()))
                {
                    soapResult = rd.ReadToEnd();
                }
                ret = soapResult;
            }
            return ret;
        }

        private static HttpWebRequest CreateWebRequest(string url, string action)
        {
            HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(url);
            webRequest.Headers.Add("SOAPAction", action);
            webRequest.ContentType = "text/xml;charset=\"utf-8\"";
            webRequest.Accept = "text/xml";
            webRequest.Method = "POST";
            return webRequest;
        }

        private static XmlDocument CreateSoapTodosEnvelope()
        {
            XmlDocument soapEnvelopeDocument = new XmlDocument();
            soapEnvelopeDocument.LoadXml(@"<SOAP-ENV:Envelope xmlns:SOAP-ENV=""http://schemas.xmlsoap.org/soap/envelope/"" xmlns:xsi=""http://www.w3.org/1999/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/1999/XMLSchema""><SOAP-ENV:Body><ConsultarTrabajadores xmlns=""http://tempuri.org/"" SOAP-ENV:encodingStyle=""http://schemas.xmlsoap.org/soap/encoding/""><sUsuario>WhiteLion</sUsuario><sPassword>T@NT@HVATA8</sPassword><sMensaje_OutPut></sMensaje_OutPut></ConsultarTrabajadores></SOAP-ENV:Body></SOAP-ENV:Envelope>");
            return soapEnvelopeDocument;
        }
        private static XmlDocument CreateSoapInactivosEnvelope()
        {
            XmlDocument soapEnvelopeDocument = new XmlDocument();
            soapEnvelopeDocument.LoadXml(@"<SOAP-ENV:Envelope xmlns:SOAP-ENV=""http://schemas.xmlsoap.org/soap/envelope/"" xmlns:xsi=""http://www.w3.org/1999/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/1999/XMLSchema""><SOAP-ENV:Body><ConsultarTrabajadoresInactivos xmlns=""http://tempuri.org/"" SOAP-ENV:encodingStyle=""http://schemas.xmlsoap.org/soap/encoding/""><sUsuario>WhiteLion</sUsuario><sPassword>N@tC1@r</sPassword><sMensaje_OutPut></sMensaje_OutPut></ConsultarTrabajadoresInactivos></SOAP-ENV:Body></SOAP-ENV:Envelope>");
            return soapEnvelopeDocument;
        }

        private static XmlDocument CreateSoapActivosEnvelope()
        {
            XmlDocument soapEnvelopeDocument = new XmlDocument();
            soapEnvelopeDocument.LoadXml(@"<SOAP-ENV:Envelope xmlns:SOAP-ENV=""http://schemas.xmlsoap.org/soap/envelope/"" xmlns:xsi=""http://www.w3.org/1999/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/1999/XMLSchema""><SOAP-ENV:Body><ConsultarTrabajadoresActivos xmlns=""http://tempuri.org/"" SOAP-ENV:encodingStyle=""http://schemas.xmlsoap.org/soap/encoding/""><sUsuario>WhiteLion</sUsuario><sPassword>N@tC1@r</sPassword><sMensaje_OutPut></sMensaje_OutPut></ConsultarTrabajadoresActivos></SOAP-ENV:Body></SOAP-ENV:Envelope>");
            return soapEnvelopeDocument;
        }
        private static void InsertSoapEnvelopeIntoWebRequest(XmlDocument soapEnvelopeXml, HttpWebRequest webRequest)
        {
            using (Stream stream = webRequest.GetRequestStream())
            {
                soapEnvelopeXml.Save(stream);
            }
        }
    }
}
