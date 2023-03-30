using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ACME.WorkerService.Tasks
{
    public interface IMiHistoriaServiceTasks
    {

        Task<object> RegistraMiHistoriaTipo1();
        Task<object> RegistraMiHistoriaTipo2();
        Task<object> RegistraMiHistoriaTipo3();



    }
}
