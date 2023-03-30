using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using ACME.Data.Contracts;
using ACME.WorkerService.Tools;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ACME.WorkerService.Tasks
{
    internal class SUNATServiceTasks : ISUNATServiceTasks
    {

        private readonly ISUNATManager _sunat;
        private readonly IConfiguration _configuration;
        private readonly ILogger _logger;
        private readonly string requestUri;
        private readonly string nombrePadron;
        private readonly string zipPadron;
        private readonly string rutaPadron;
        private readonly string filePadron;
        private readonly int limit;

        public SUNATServiceTasks(ISUNATManager sunat, ILogger<SUNATProcessingService> logger, IConfiguration configuration)
        {
            _sunat = sunat;
            _configuration = configuration;
            _logger = logger;

            requestUri = @_configuration["WORKER_SUNATServices:UrlPadron"];
            nombrePadron = $"padron_{DateTime.Now:yyyy_MM_dd_HH_mm_ss}";
            zipPadron = System.IO.Path.Combine(@_configuration["WORKER_SUNATServices:RepoPadron"], $"{nombrePadron}.zip");
            rutaPadron = System.IO.Path.Combine(@_configuration["WORKER_SUNATServices:RepoPadron"], nombrePadron);
            filePadron = System.IO.Path.Combine(@rutaPadron, @_configuration["WORKER_SUNATServices:FilePadron"]);
            //filePadron = Path.Combine(@_configuration["WORKER_SUNATServices:RepoPadron"], "padron_1.txt");
            limit = int.Parse(@_configuration["WORKER_SUNATServices:LimitRows"]);
        }


        public async Task<object> ReprocesamensajesMQ()
        {
            try
            {
                
            }
            catch (Exception ex) { _logger.LogError(ex.Message); }
            return true;
        }

        #region Padrón

        public async Task<object> RegistrarPadron()
        {
            _logger.LogInformation("-----> RegistrarPadron inicio");
            _logger.LogInformation($"Descargar zip del padrón de {requestUri}.");

            HttpClient httpClient = new HttpClient();
            Stream stream = await httpClient.GetStreamAsync(requestUri);

            using (var fileStream = File.Create(zipPadron))
            {
                await stream.CopyToAsync(fileStream);
            }

            _logger.LogInformation($"Padrón descargado en  {zipPadron}.");

            //ZipFile.ExtractToDirectory(zipPadron, rutaPadron, Encoding.UTF8);
            ZipFile.ExtractToDirectory(zipPadron, rutaPadron);

            _logger.LogInformation($"Padrón descomprimido en {filePadron}.");

            _logger.LogInformation($"Padrón {filePadron} convirtiendo a UTF8.");

            _logger.LogInformation($"Proceso iniciado del registro del padrón {filePadron}.");

            await ReadFileInBatch(filePadron);

            _logger.LogInformation($"Proceso finalizado del registro del padrón {filePadron}.");

            FileInfo file = new FileInfo(zipPadron);
            if (file.Exists)
            {
                file.Delete();
            }

            DirectoryInfo di = new DirectoryInfo(rutaPadron);
            foreach (DirectoryInfo dir in di.GetDirectories())
            {
                foreach (FileInfo f in dir.GetFiles())
                {
                    f.Delete();
                }

                dir.Delete(true);
            }

            _logger.LogInformation($"Eliminar zip y archivo del padrón {filePadron}.");
            _logger.LogInformation("-----> RegistrarPadron fin");

            return true;
        }
        /*
        private static void ConvertAnsiToUTF8(string inputFilePath, string outputFilePath)
        {
            string fileContent = File.ReadAllText(inputFilePath, Encoding.Default);
            File.WriteAllText(outputFilePath, fileContent, Encoding.UTF8);
        }
        public static String readFileAsUtf8(string fileName)
        {
            Encoding encoding = Encoding.Default;
            String original = String.Empty;

            using (StreamReader sr = new StreamReader(fileName, Encoding.Default))
            {
                original = sr.ReadToEnd();
                encoding = sr.CurrentEncoding;
                sr.Close();
            }

            if (encoding == Encoding.UTF8)
                return original;

            byte[] encBytes = encoding.GetBytes(original);
            byte[] utf8Bytes = Encoding.Convert(encoding, Encoding.UTF8, encBytes);
            return Encoding.UTF8.GetString(utf8Bytes);
        }

        void CopyTo(Stream from, Stream to)
        {
            byte[] buffer = new byte[0x10000];
            int read = from.Read(buffer, 0, buffer.Length);
            while (read > 0)
            {
                to.Write(buffer, 0, read); 
                read = from.Read(buffer, 0, buffer.Length);
            }
        }
        */
        //public static void SplitFile(string inputFile, int chunkSize, string path)
        //{
        //    const int BUFFER_SIZE = 20 * 1024;
        //    byte[] buffer = new byte[BUFFER_SIZE];

        //    using Stream input = File.OpenRead(inputFile);
        //    int index = 0;
        //    while (input.Position < input.Length)
        //    {
        //        using (Stream output = File.Create(path + "\\" + index))
        //        {
        //            int remaining = chunkSize, bytesRead;
        //            while (remaining > 0 && (bytesRead = input.Read(buffer, 0,
        //                    Math.Min(remaining, BUFFER_SIZE))) > 0)
        //            {
        //                output.Write(buffer, 0, bytesRead);
        //                remaining -= bytesRead;
        //            }
        //        }
        //        index++;
        //        Thread.Sleep(500); // experimental; perhaps try it
        //    }
        //}

        #endregion Padrón

        internal async Task ReadFileInBatch(string path)
        {
            System.Text.EncodingProvider provider = System.Text.CodePagesEncodingProvider.Instance;
            Encoding.RegisterProvider(provider);


            int TotalRows = File.ReadLines(path).Count();
            //for (int Offset = _Offset; Offset < TotalRows; Offset += Limit)
            for (int Offset = 0; Offset < TotalRows; Offset += limit)
            {
                string Logs = string.Format("Processing :: Rows {0} of Total {1} :: Offset {2} : Limit : {3}", (Offset + limit) < TotalRows ? Offset + limit : TotalRows, TotalRows, Offset, limit);

                _logger.LogInformation(Logs);
                try
                {
                    //var table = path.FileToTable(logger: _logger, heading: true, delimiter: '|', offset: Offset, limit: limit);
                    var table = path.FileToTable(heading: true, delimiter: '|', offset: Offset, limit: limit);

                    await _sunat.RegistraPadron(table, Offset, limit);
                }
                catch (Exception ex)
                {
                    _logger.LogCritical(ex.StackTrace);
                }
            }
        }

    }
}
