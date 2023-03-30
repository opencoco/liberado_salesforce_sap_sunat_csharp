using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
//using Blackmore.Cms.Data.Contracts;
//using Blackmore.Cms.Tools;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;


namespace ACME.WorkerService
{
    public class WorkerSAPAsientos : BackgroundService
    {
        private readonly ILogger<WorkerSAPAsientos> _logger;
        //private readonly IServiceScopeFactory _scopeFactory;
        public IServiceProvider _service { get; }
        public string LogName { get; internal set; }
        public string SourceName { get; internal set; }

        public WorkerSAPAsientos(IServiceProvider services, ILogger<WorkerSAPAsientos> logger)
        {
            _service = services;
            _logger = logger;
        }

        public override async Task StartAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Worker SAP Asientos start");

            await base.StartAsync(stoppingToken);
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Consume Scoped SAP Asientos Service Hosted Service running.");

            await DoWork(stoppingToken);
        }

        private async Task DoWork(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Consume Scoped SAP Asientos Service Hosted Service is working.");
            while (!stoppingToken.IsCancellationRequested)
            {
                using var scope = _service.CreateScope();
                var sapasientos = _service.CreateScope().ServiceProvider.GetRequiredService<ISAPAsientosService>();
                await sapasientos.DoWork(stoppingToken);
            }
        }

        public override async Task StopAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Consume Scoped SAP Asientos Service Hosted Service is stopping.");

            await Task.CompletedTask;
        }
    }
}
