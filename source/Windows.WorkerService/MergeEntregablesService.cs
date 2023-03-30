using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using ACME.WorkerService.Tasks;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ACME.WorkerService
{
    internal interface IMergeEntregablesService
    {
        Task DoWork(CancellationToken stoppingToken);
    }

    internal class MergeEntregablesService : IMergeEntregablesService
    {
        private int executionCount = 0;
        private readonly IMergeEntregablesTasks _tasks;
        private readonly ILogger _logger;
        private readonly int runIntervallInMinutes;

        public MergeEntregablesService(IMergeEntregablesTasks tasks, ILogger<MergeEntregablesService> logger, IConfiguration configuration)
        {
            _tasks = tasks;
            _logger = logger;
            runIntervallInMinutes = int.Parse(configuration.GetSection("WORKER_MergeEntregables:RunIntervallInMinutes").Value);
        }

        public async Task DoWork(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                executionCount++;
                _logger.LogInformation("MergeEntregablesService is working. Count: {Count}", executionCount);

                await _tasks.ListarEpisodiosColaMerge();
                //_tasks.TestMerge();

                _logger.LogInformation("---> GenerarEntregablesPendientes is working. Count: {Count}", executionCount);

                await Task.Delay(TimeSpan.FromMinutes(runIntervallInMinutes), stoppingToken);
            }
        }

    }
}
