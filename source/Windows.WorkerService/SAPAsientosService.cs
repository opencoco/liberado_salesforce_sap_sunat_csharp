using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using ACME.WorkerService.Tasks;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ACME.WorkerService
{
    internal interface ISAPAsientosService
    {
        Task DoWork(CancellationToken stoppingToken);
    }

    internal class SAPAsientosService : ISAPAsientosService
    {
        private int executionCount = 0;
        private readonly ISAPAsientosTasks _tasks;
        private readonly ILogger _logger;
        private readonly int runIntervallInMinutes;

        public SAPAsientosService(ISAPAsientosTasks tasks, ILogger<SAPAsientosService> logger, IConfiguration configuration)
        {
            _tasks = tasks;
            _logger = logger;
            runIntervallInMinutes = int.Parse(configuration.GetSection("WORKER_SAPAsientoServices:RunIntervallInMinutes").Value);
        }

        public async Task DoWork(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                executionCount++;
                _logger.LogInformation("SAPAsientosService is working. Count: {Count}", executionCount);

                await _tasks.GenerarAsientosPendientes();
                
                _logger.LogInformation("---> SAPAsientosService is working. Count: {Count}", executionCount);

                await Task.Delay(TimeSpan.FromMinutes(runIntervallInMinutes), stoppingToken);
            }
        }

    }
}
