using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ACME.WorkerService.Tasks
{
    public interface ISalesForceTasks
    {
        Task<object> SF_ActualizaCita();
        Task<object> GenerarParquet();
        Task<object> SF_EnviasDatosMedicos();
        Task<object> SF_EnviasDatosAltaPaciente();
        Task<object> SF_EnviaBajaPacientes();
        Task<object> SF_SincronizaHeadcount();

    }
}
