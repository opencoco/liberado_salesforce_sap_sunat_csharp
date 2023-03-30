using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACME.WorkerService.Helpers
{
    public interface ICloudStorage
    {
        Task<string> UploadFileAsync(MemoryStream file, string fileNameForStorage);
        Task<Stream> DowloadFileAsync(string fileNameForStorage);
        Task DeleteFileAsync(string fileNameForStorage);
        Task<bool> SaveFileinLocalAsync(MemoryStream file, string fileNameForStorage);
    }
}
