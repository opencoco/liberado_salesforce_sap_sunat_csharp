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
    internal interface ISUNATProcessingService
    {
        Task DoWork(CancellationToken stoppingToken);
    }

    internal class SUNATProcessingService : ISUNATProcessingService
    {
        private int executionCount = 0;
        private readonly ISUNATServiceTasks _tasks;
        private readonly ILogger _logger;
        private readonly int runIntervallInHours;

        public SUNATProcessingService(ISUNATServiceTasks tasks, ILogger<SUNATProcessingService> logger, IConfiguration configuration)
        {
            _tasks = tasks;
            _logger = logger;
            runIntervallInHours = int.Parse(configuration.GetSection("WORKER_SUNATServices:RunIntervallInHours").Value);
        }

        public async Task DoWork(CancellationToken stoppingToken)
        {

            var now = DateTime.Now;
            var hours = 23 - now.Hour;
            var minutes = 59 - now.Minute;
            var seconds = 59 - now.Second;
            var secondsTillMidnight = hours * 3600 + minutes * 60 + seconds;

            //await Task.Delay(TimeSpan.FromSeconds(secondsTillMidnight), stoppingToken);

            while (!stoppingToken.IsCancellationRequested)
            {
                //var currentTime = DateTime.Now;
                //if (currentTime.Hour >= 23 && currentTime.Hour <= 2)
                //{
                executionCount++;
                _logger.LogInformation("SUNAT Processing Service is working. Count: {Count}", executionCount);
                try
                {
                    await _tasks.RegistrarPadron();
                    _logger.LogInformation("---> RegistrarPadron is working. Count: {Count}", executionCount);

                    await _tasks.ReprocesamensajesMQ();
                }
                catch (Exception ex) { _logger.LogError(ex.Message); }
                //await Task.Delay(TimeSpan.FromHours(runIntervallInHours), stoppingToken);
                // wait till midnight
                await Task.Delay(TimeSpan.FromSeconds(secondsTillMidnight), stoppingToken);
                //}
            }

        }

    }
}
