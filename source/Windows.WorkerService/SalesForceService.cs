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
    internal interface ISalesForceService
    {
        Task DoWork(CancellationToken stoppingToken);
    }

    internal class SalesForceService : ISalesForceService
    {
        private int executionCount = 0;
        private readonly ISalesForceTasks _tasks;
        private readonly ILogger _logger;
        private readonly int runIntervallInMinutes;

        public SalesForceService(ISalesForceTasks tasks, ILogger<SalesForceService> logger, IConfiguration configuration)
        {
            _tasks = tasks;
            _logger = logger;
            runIntervallInMinutes = int.Parse(configuration.GetSection("WORKER_Parquet:RunIntervallInMinutes").Value);
        }

        public async Task DoWork(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                executionCount++;
                _logger.LogInformation("GenerarBNV - SF Service is working. Count: {Count}", executionCount);
                await _tasks.SF_SincronizaHeadcount();
                await _tasks.SF_ActualizaCita();
                await _tasks.GenerarParquet();
                await _tasks.SF_EnviasDatosAltaPaciente();
                await _tasks.SF_EnviasDatosMedicos();
                await _tasks.SF_EnviaBajaPacientes();
                

                _logger.LogInformation("---> GenerarBNV - SF is working. Count: {Count}", executionCount);

                await Task.Delay(TimeSpan.FromMinutes(runIntervallInMinutes), stoppingToken);
            }
        }

    }
}
