using Microsoft.Extensions.FileProviders;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ACME.WorkerService.Tools
{
    public class Helper
    {
        private static PhysicalFileProvider _fileProvider = new(Directory.GetCurrentDirectory());
        //private static JsonDocumentOptions options = new JsonDocumentOptions
        //{
        //    AllowTrailingCommas = true
        //};

        public static byte[] executeRequestSalesForce(string url, string method, string contentType, string accessToken, byte[] body)
        {
            try
            {
                System.Net.HttpWebRequest req =
                    System.Net.WebRequest.Create(url) as HttpWebRequest;

                if (accessToken != null) req.Headers["Authorization"] = "Bearer " + accessToken;

                req.Method = method;

                req.ContentType = contentType;

                if (body != null) req.GetRequestStream().Write(body, 0, body.Length);

                System.Net.WebResponse res = null;
                try
                {
                    res = req.GetResponse();
                }
                catch (System.Net.WebException e)
                {
                    res = e.Response;
                }

                using (MemoryStream mem = new MemoryStream())
                using (Stream resStream = res.GetResponseStream())
                {
                    byte[] buf = new byte[4096];
                    int r;
                    while ((r = resStream.Read(buf, 0, buf.Length)) > 0)
                    {
                        mem.Write(buf, 0, r);
                    }
                    return mem.ToArray();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
            }
            return null;
        }

        public static async Task<ValidateResponse> ValidatorJson(string jsonValidator, string jsonContent)
        {
            string json_schema = await GetJsonFile($"Json/{jsonValidator}");            
            JSchema schema = JSchema.Parse(json_schema);
            JToken json = JToken.Parse(jsonContent);

            bool valid = json.IsValid(schema, out IList<ValidationError> errors);

            return new ValidateResponse
            {
                Valid = valid,
                Errors = errors
            };
        }

        public static async Task<string> GetJsonFile(string filePath)
        {
            IFileInfo fileInfo = _fileProvider.GetFileInfo(@filePath);
            string conte = await GetFile(fileInfo.PhysicalPath);

            return conte;
        }

        public static (HttpStatusCode statusCode, string resultContent) Request(object data, string url, string accessToken, string type_method)
        {
           
            HttpResponseMessage result = null;
            using var client = new HttpClient();
            if (!string.IsNullOrEmpty(accessToken))
            {
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + accessToken);
            }

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
            //dynamic jsonData = new JavaScriptSerializer().DeserializeObject(resultContent);
            //dynamic jsonData = JObject.Parse(resultContent);

            return (result.StatusCode, resultContent);
        }

        #region privates methods

        private static async Task<string> GetFile(string filePath)
        {
            string conte = null;

            if (System.IO.File.Exists(filePath))
            {
                conte = System.IO.File.ReadAllText(filePath, Encoding.GetEncoding("iso-8859-1"));
            }

            return await Task.FromResult(conte);
        }

        #endregion privates methods
    }

    //public class ValidateRequest
    //{
    //    public string Json { get; set; }
    //    public string Schema { get; set; }
    //}

    public class ValidateResponse
    {
        public bool Valid { get; set; }
        public IList<ValidationError> Errors { get; set; }
    }

    //internal class Util
    //{
    //    internal Util() { }

    //    public (HttpStatusCode statusCode, string resultContent) Request(object data, string url, string api_key, string type_method)
    //    {
    //        HttpResponseMessage result = null;
    //        using var client = new HttpClient();
    //        //client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", api_key);
    //        if (!string.IsNullOrEmpty(api_key))
    //        {
    //            client.DefaultRequestHeaders.Add("X-Auth-Token", api_key);
    //        }
    //        //client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

    //        if (type_method.Equals("get"))
    //        {
    //            result = client.GetAsync(url).Result;
    //        }
    //        else if (type_method.Equals("post"))
    //        {
    //            var json = JsonConvert.SerializeObject(data);
    //            var content = new StringContent(json, Encoding.UTF8, "application/json");
    //            result = client.PostAsync(url, content).Result;
    //        }
    //        else if (type_method.Equals("put"))
    //        {
    //            var json = JsonConvert.SerializeObject(data);
    //            var content = new StringContent(json, Encoding.UTF8, "application/json");
    //            result = client.PutAsync(url, content).Result;
    //        }
    //        else if (type_method.Equals("delete"))
    //        {
    //            result = client.DeleteAsync(url).Result;
    //        }

    //        if (result.StatusCode != HttpStatusCode.Created)
    //        {

    //        }

    //        string resultContent = result.Content.ReadAsStringAsync().Result;
    //        //dynamic jsonData = new JavaScriptSerializer().DeserializeObject(resultContent);
    //        //dynamic jsonData = JObject.Parse(resultContent);

    //        return (result.StatusCode, resultContent);
    //    }
    }

