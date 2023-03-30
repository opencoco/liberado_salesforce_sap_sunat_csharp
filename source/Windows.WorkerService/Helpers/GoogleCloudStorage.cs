using Google.Apis.Auth.OAuth2;
using Google.Cloud.Storage.V1;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ACME.WorkerService.Helpers
{
    public class GoogleCloudStorage : ICloudStorage
    {
        private readonly GoogleCredential googleCredential;
        private readonly StorageClient storageClient;
        private readonly string bucketName;
        private readonly string localdrive;
        private const int DefaultBufferSize = 4096;
        public GoogleCloudStorage(IConfiguration configuration)
        {
            try
            {
                googleCredential = GoogleCredential.FromFile(configuration.GetValue<string>("CloudStorage:GoogleCredentialFile"));
                storageClient = StorageClient.Create(googleCredential);
                bucketName = configuration.GetValue<string>("CloudStorage:GoogleCloudStorageBucket");
                localdrive = configuration.GetValue<string>("CloudStorage:GoogleCloudStorageLocal");
            }
            catch (Exception ex)
            {
                //_logger.LogError(ex.Message);
            }
        }

        public async Task<string> UploadFileAsync(MemoryStream file, string fileNameForStorage)
        {
            //using (var memoryStream = new MemoryStream())
            //{
            //await imageFile.CopyToAsync(memoryStream);
            var dataObject = await storageClient.UploadObjectAsync(bucketName, fileNameForStorage, null, file);
            return dataObject.MediaLink;
            //}
        }

        public async Task<Stream> DowloadFileAsync(string fileNameForStorage)
        {
            //string fileDownloadPath = @"C:\Test\gcp\cloud\CloudBlobTest_Out.pdf";
            //string objectBlobName = "CloudBlobTest.pdf";
            //var gcsStorage = StorageClient.Create();
            //using var outputFile = File.OpenWrite(fileDownloadPath);
            //gcsStorage.DownloadObject(bucketName, objectBlobName, outputFile);

            //Stream destination = new MemoryStream();
            //await storageClient.DownloadObjectAsync(bucketName, fileNameForStorage, destination);

            WebClient myWebClient = new();
            byte[] file = await myWebClient.DownloadDataTaskAsync(fileNameForStorage);
            MemoryStream destination = new(file) { Position = 0 };
            //Stream destination = Convert.ToBase64String(file.ToArray());

            return destination;
        }

        public async Task<bool> SaveFileinLocalAsync(MemoryStream file, string fileNameForStorage)
        {
            string filePath = localdrive + fileNameForStorage;
            var f = file.ToArray();
            using FileStream sourceStream = new FileStream(filePath, FileMode.Append, FileAccess.Write, FileShare.None,
            DefaultBufferSize, true);
            await sourceStream.WriteAsync(f, 0, f.Length);
            return true;
        }

        public async Task DeleteFileAsync(string fileNameForStorage)
        {
            await storageClient.DeleteObjectAsync(bucketName, fileNameForStorage);
        }
    }
}
