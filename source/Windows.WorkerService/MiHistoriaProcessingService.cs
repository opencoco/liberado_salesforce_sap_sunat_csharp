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
    internal interface IMiHistoriaProcessingService
    {
        Task DoWork(CancellationToken stoppingToken);
    }

    internal class MiHistoriaProcessingService : IMiHistoriaProcessingService
    {
        private int executionCount = 0;
        private readonly IMiHistoriaServiceTasks _tasks;
        private readonly ILogger _logger;
        private readonly int runIntervallInMinutes;

        public MiHistoriaProcessingService(IMiHistoriaServiceTasks tasks, ILogger<MiHistoriaProcessingService> logger, IConfiguration configuration)
        {
            _tasks = tasks;
            _logger = logger;
            runIntervallInMinutes = int.Parse(configuration.GetSection("WORKER_MiHistoriaServices:RunIntervallInMinutes").Value);
        }

        public async Task DoWork(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                executionCount++;
                _logger.LogInformation("MIHISTORIA Processing Service is working. Count: {Count}", executionCount);

                await _tasks.RegistraMiHistoriaTipo1();
                _logger.LogInformation("---> RegistraMiHistoriaTipo1 is working. Count: {Count}", executionCount);

                await _tasks.RegistraMiHistoriaTipo2();
                _logger.LogInformation("---> RegistraMiHistoriaTipo2 is working. Count: {Count}", executionCount);

                await _tasks.RegistraMiHistoriaTipo3();
                _logger.LogInformation("---> RegistraMiHistoriaTipo3 is working. Count: {Count}", executionCount);

                await Task.Delay(TimeSpan.FromMinutes(runIntervallInMinutes), stoppingToken);
            }
        }

    }
}
