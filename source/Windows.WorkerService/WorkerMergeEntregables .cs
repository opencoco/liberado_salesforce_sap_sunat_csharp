using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;


namespace ACME.WorkerService
{
    public class WorkerMergeEntregables : BackgroundService
    {
        private readonly ILogger<WorkerMergeEntregables> _logger;
        public IServiceProvider _service { get; }
        public string LogName { get; internal set; }
        public string SourceName { get; internal set; }

        public WorkerMergeEntregables(IServiceProvider services, ILogger<WorkerMergeEntregables> logger)
        {
            _service = services;
            _logger = logger;
        }

        public override async Task StartAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Worker Merge Entregables start");

            await base.StartAsync(stoppingToken);
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Consume Scoped Merge Entregables Service Hosted Service running.");

            await DoWork(stoppingToken);
        }

        private async Task DoWork(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Consume Scoped Merge Entregables Service Hosted Service is working.");
            while (!stoppingToken.IsCancellationRequested)
            {
                using var scope = _service.CreateScope();
                var MergeEntregablesService = _service.CreateScope().ServiceProvider.GetRequiredService<IMergeEntregablesService>();
                await MergeEntregablesService.DoWork(stoppingToken);
            }
        }

        public override async Task StopAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Consume Scoped Merge Entregables Service Hosted Service is stopping.");

            await Task.CompletedTask;
        }
    }
}
