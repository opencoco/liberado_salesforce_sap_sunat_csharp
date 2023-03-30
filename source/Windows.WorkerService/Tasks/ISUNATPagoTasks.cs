using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ACME.WorkerService.Tasks
{
    public interface ISUNATPagoTasks
    {
        #region Padrón
        //Task<object> GenerarEntregablesPendientes();
        Task<object> RegistrarComprobantes();
        Task<object> RecuperarNotasCredito();

        #endregion Padrón


    }
}
