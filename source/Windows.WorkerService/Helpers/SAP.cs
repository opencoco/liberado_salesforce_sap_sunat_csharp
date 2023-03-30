using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using RestSharp;


namespace ACME.WorkerService.Helpers
{
    class SAP
    {
        private RestClient client;
        private RestRequest request;

        public string BPUPdate(string URL, string body,string cardcode)
        {
            var requestx = (HttpWebRequest)WebRequest.Create(URL);
            var data = Encoding.ASCII.GetBytes($"format=json&record={body}");
            if (!string.IsNullOrEmpty(cardcode)) data = Encoding.ASCII.GetBytes($"id={cardcode}&format=json&record={body}");

            requestx.Accept = "application/json, text/javascript, */*; q=0.01";
            requestx.Method = "POST";
            requestx.ContentType = "text/xml";
            requestx.ContentLength = data.Length;
            using (var stream = requestx.GetRequestStream()) { stream.Write(data, 0, data.Length); }

            var responsex = new StreamReader(((HttpWebResponse)requestx.GetResponse()).GetResponseStream()).ReadToEnd();
            return responsex;
        }
    }
}
