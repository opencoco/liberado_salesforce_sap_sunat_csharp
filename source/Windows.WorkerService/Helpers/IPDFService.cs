using System.Collections.Generic;
using System.IO;

namespace ACME.WorkerService.Helpers
{
    public interface IPDFService
    {
        //byte[] MergeFiles(List<Stream> sourceFiles);
        byte[] GenerarPdfStream(string html);
        string Base64QR(string texto);
        byte[] GenerarMergePdfStream(IList<Stream> files, byte[]? digitalsign, string clave);
        void GenerarMergePdfFile(string destinationFile, IList<string> files);
    }
}
