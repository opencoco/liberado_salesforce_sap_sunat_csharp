using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ACME.WorkerService.Tasks
{
    public interface ISUNATServiceTasks
    {
        #region Padrón
        Task<object> ReprocesamensajesMQ();
        Task<object> RegistrarPadron();

        #endregion Padrón


    }
}
