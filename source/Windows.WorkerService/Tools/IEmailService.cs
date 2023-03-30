using System.Collections.Generic;
using System.Threading.Tasks;
using System.IO;
using System.Threading;

namespace ACME.WorkerService.Tools
{
    public interface IEmailService
    {
        Task SendEmail(string email, int IDPlantilla, Dictionary<string, string> Datos, MemoryStream attachfile, string filename, CancellationToken cancellationToken = default);
    }
}
