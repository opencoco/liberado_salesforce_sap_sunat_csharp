using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using ACME.Data.Contracts;
using ACME.WorkerService.Helpers;
using ACME.WorkerService.Tasks;
using ACME.WorkerService.Tools;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ACME.WorkerService
{
    internal interface ISUNATPagoService
    {
        Task DoWork(CancellationToken stoppingToken);
    }

    internal class SUNATPagoService : ISUNATPagoService
    {
        private int executionCount = 0;
        private readonly ISUNATPagoTasks _tasks;
        private readonly ILogger _logger;
        private readonly int runIntervallInMinutes;
        

        public SUNATPagoService(ISUNATPagoTasks tasks, ILogger<SUNATPagoService> logger, IConfiguration configuration)
        {
            _tasks = tasks;
            _logger = logger;
            runIntervallInMinutes = int.Parse(configuration.GetSection("WORKER_SUNATPago:RunIntervallInMinutes").Value);
            
        }

        public async Task DoWork(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                executionCount++;
                _logger.LogInformation("SUNAT Pago Service is working. Count: {Count}", executionCount);

                await _tasks.RegistrarComprobantes();
                await _tasks.RecuperarNotasCredito();
                //await _tasks.GenerarEntregablesPendientes();
                _logger.LogInformation("---> RegistrarComprobantes is working. Count: {Count}", executionCount);

                //await _tasks.RegistrarComprobanteDigiflow();
                //_logger.LogInformation("---> RegistrarComprobanteDigiflow is working. Count: {Count}", executionCount);

                await Task.Delay(TimeSpan.FromMinutes(runIntervallInMinutes), stoppingToken);
            }
        }

    }
}
