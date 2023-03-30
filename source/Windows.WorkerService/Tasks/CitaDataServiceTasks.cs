using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using ACME.Data.Contracts;
using ACME.WorkerService.Tools;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ACME.WorkerService.Tasks
{
    internal class CitaDataServiceTasks : ICitaDataServiceTasks
    {

        private readonly ICitaTrabajadorManager _cita;
        private readonly IEmailService _emailService;
        private readonly IConfiguration _configuration;
        private readonly ILogger _logger;
        //private readonly ILogger<ServiceTasks> _logger;

        public CitaDataServiceTasks(ICitaTrabajadorManager cita, IEmailService emailService, ILogger<CitaDataProcessingService> logger, IConfiguration configuration)
        {
            _cita = cita;
            _emailService = emailService;
            _configuration = configuration;
            _logger = logger;
        }

        #region Citados

        public async Task<object> TransformarDynamicForms()
        {
            _logger.LogInformation("-----> SincronizaCitaData inicio");
            await _cita.SincronizaCitaData();
            _logger.LogInformation("-----> SincronizaCitaData fin");

            return true;
        }

        #endregion Citados


        #region Atendidos

        public Task<object> GenerarAtenciones()
        {
            throw new NotImplementedException();
        }

        #endregion Atendidos


        #region Valorizacion

        public Task<object> RevisarValorizaciones()
        {
            throw new NotImplementedException();
        }

        #endregion Valorizacion


        #region Facturados

        public Task<object> GenerarFacturaciones()
        {
            throw new NotImplementedException();
        }

        #endregion Facturados


        #region Pagados

        public Task<object> NotificarPagados()
        {
            throw new NotImplementedException();
        }

        #endregion Pagados

    }
}
