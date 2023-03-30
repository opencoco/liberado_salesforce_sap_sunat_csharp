using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using ACME.Data.Contracts;
using ACME.WorkerService.Tasks;
using ACME.WorkerService.Tools;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ACME.WorkerService
{
    internal interface ICitaProcessingService
    {
        Task DoWork(CancellationToken stoppingToken);
        //Task ClearAllRecycleBin(CancellationToken stoppingToken);
    }

    internal class CitaDataProcessingService : ICitaProcessingService
    {
        private int executionCount = 0;
        private readonly ICitaDataServiceTasks _tasks;
        private readonly IEmailService _emailService;
        private readonly IConfiguration _configuration;
        private readonly ILogger _logger;
        private readonly int runIntervallInMinutes;

        public CitaDataProcessingService(ICitaDataServiceTasks tasks, IEmailService emailService, ILogger<CitaDataProcessingService> logger, IConfiguration configuration)
        {
            _tasks = tasks;
            _emailService = emailService;
            _configuration = configuration;
            _logger = logger;
            runIntervallInMinutes = int.Parse(configuration.GetSection("WORKER_CitaServices:RunIntervallInMinutes").Value);
        }

        public async Task DoWork(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                executionCount++;
                _logger.LogInformation("CITADATA Processing Service is working. Count: {Count}", executionCount);

                await _tasks.TransformarDynamicForms();
                _logger.LogInformation("---> TransformarDynamicForms is working. Count: {Count}", executionCount);

                //string subject = "SendTestForm from ACME.WorkerService";
                //string[] to = new string[] { "miguelangelhurtado@hotmail.com" };
                //string registeredForm = "This is the content from our async email.";
                //await SendTestEmail(to, subject, registeredForm, stoppingToken);

                await Task.Delay(TimeSpan.FromMinutes(runIntervallInMinutes), stoppingToken);
            }
        }

        private async Task SendTestEmail(string[] to, string subject, string content, CancellationToken stoppingToken = default)
        {
            Dictionary<string, string> dicDatos = new Dictionary<string, string>
            {
                { "Solicitante", "Miguel" },
                { "TareaNombre", "Tarea test" },
                { "WorkflowID", "test" },
                { "TareaComentarios", "test" },
                { "URL", string.Format("{0}/web/mis-pendientes/{1}", @_configuration["webroot"], "1234") }
            };

            await _emailService.SendEmail("miguelangelhurtado@hotmail.com", 5, dicDatos, null, "", stoppingToken);
        }

    }
}
