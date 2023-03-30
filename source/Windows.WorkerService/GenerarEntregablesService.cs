using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using ACME.WorkerService.Tasks;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ACME.WorkerService
{
    internal interface IGenerarEntregablesService
    {
        Task DoWork(CancellationToken stoppingToken);
    }

    internal class GenerarEntregablesService : IGenerarEntregablesService
    {
        private int executionCount = 0;
        private readonly IGenerarEntregablesTasks _tasks;
        private readonly ILogger _logger;
        private readonly int runIntervallInMinutes;

        public GenerarEntregablesService(IGenerarEntregablesTasks tasks, ILogger<GenerarEntregablesService> logger, IConfiguration configuration)
        {
            _tasks = tasks;
            _logger = logger;
            runIntervallInMinutes = int.Parse(configuration.GetSection("WORKER_GenerarEntregables:RunIntervallInMinutes").Value);
        }

        public async Task DoWork(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                executionCount++;
                _logger.LogInformation("GenerarEntregablesService is working. Count: {Count}", executionCount);

                await _tasks.GenerarEntregablesPendientes();
                
                _logger.LogInformation("---> GenerarEntregablesPendientes is working. Count: {Count}", executionCount);

                await Task.Delay(TimeSpan.FromMinutes(runIntervallInMinutes), stoppingToken);
            }
        }

    }
}
