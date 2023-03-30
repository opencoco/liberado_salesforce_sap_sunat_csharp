using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ACME.WorkerService.Tasks
{
    public interface ICitaDataServiceTasks
    {
        #region Citados
        //Task<object> TransformarDynamicForms(CancellationToken stoppingToken);
        Task<object> TransformarDynamicForms();

        #endregion Citados


        #region Atendidos

        Task<object> GenerarAtenciones();

        #endregion Atendidos


        #region Valorizacion

        Task<object> RevisarValorizaciones();

        #endregion Valorizacion


        #region Facturados

        Task<object> GenerarFacturaciones();

        #endregion Facturados


        #region Pagados

        Task<object> NotificarPagados();

        #endregion Pagados
    }
}
