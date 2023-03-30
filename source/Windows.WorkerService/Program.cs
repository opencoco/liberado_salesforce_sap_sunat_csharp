using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.EventLog;
using ACME.WorkerService.Helpers;
using Serilog;

namespace ACME.WorkerService
{
    public class Program
    {
        //TasksSettings tasks_settings;
        public static async Task Main(string[] args)
        {
            var configuration = new ConfigurationBuilder()
              .AddJsonFile("appsettings.logs.json")
              .Build();

            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(configuration)
                .MinimumLevel.Debug()
                .WriteTo.Console()
                .CreateLogger();

            try
            {
                Log.Information("Starting ACME.WorkerService");
                var host = CreateHostBuilder(args).UseSerilog().Build();

                await host.StartAsync();

                // Monitor for new background queue work items
                #region snippet4
                //var monitorLoop = host._service.GetRequiredService<MonitorLoop>();
                //monitorLoop.StartMonitorLoop();
                #endregion

                // Wait for the host to shutdown
                await host.WaitForShutdownAsync();
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Host terminated unexpectedly");
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
            .UseWindowsService() // para windows
            .UseSystemd() // para linux
            .ConfigureLogging(options =>
                options.AddFilter(level => level >= LogLevel.Error)
            )
            .ConfigureServices((hostContext, services) =>
            {

                var configuration = new ConfigurationBuilder()
                    .AddJsonFile("appsettings.json")
                    .Build();

                var tasks_settings = new TasksSettings();
                configuration.GetSection("Tasks").Bind(tasks_settings);

                services.ConfigureEmailService(hostContext.Configuration);
                services.ConfigureRepositoryWrapper();

                services.AddSingleton<ICloudStorage, GoogleCloudStorage>();
                services.AddTransient<IPDFService, PDFService>();

                #region snippet

                if (tasks_settings.citas == true)
                {
                    services.AddHostedService<WorkerCitaData>()
                    .Configure<WorkerCitaData>(config =>
                    {
                        config.LogName = "ACME.WorkerService CITADATA";
                        config.SourceName = "ACME.WorkerService CITADATA";
                    });
                }

                if (tasks_settings.sunat_padron == true)
                {
                    services.AddHostedService<WorkerSUNAT>()
                    .Configure<WorkerSUNAT>(config =>
                    {
                        config.LogName = "ACME.WorkerService SUNAT";
                        config.SourceName = "ACME.WorkerService SUNAT";
                    });
                }

                if (tasks_settings.mihistoria == true)
                {
                    services.AddHostedService<WorkerMiHistoria>()
                    .Configure<WorkerMiHistoria>(config =>
                    {
                        config.LogName = "ACME.WorkerService MIHISTORIA";
                        config.SourceName = "ACME.WorkerService MIHISTORIA";
                    });
                }

                if (tasks_settings.sunat_pago == true)
                {
                    services.AddHostedService<WorkerSUNATPago>()
                    .Configure<WorkerSUNATPago>(config =>
                    {
                        config.LogName = "ACME.WorkerService SUNAT PAGO";
                        config.SourceName = "ACME.WorkerService SUNAT PAGO";
                    });
                }

                if (tasks_settings.generar_entregables == true)
                {
                    services.AddHostedService<WorkerGenerarEntregables>()
                    .Configure<WorkerGenerarEntregables>(config =>
                    {
                        config.LogName = "ACME.WorkerService Generar Entregables";
                        config.SourceName = "ACME.WorkerService Generar Entregables";
                    });
                }

                if (tasks_settings.merge_entregables == true)
                {
                    services.AddHostedService<WorkerMergeEntregables>()
                    .Configure<WorkerMergeEntregables>(config =>
                    {
                        config.LogName = "ACME.WorkerService Merge Entregables";
                        config.SourceName = "ACME.WorkerService Merge Entregables";
                    });
                }

                if (tasks_settings.parquet_bnv == true)
                {
                    services.AddHostedService<WorkerSalesForce>()
                    .Configure<WorkerSalesForce>(config =>
                    {
                        config.LogName = "ACME.WorkerService BNV Sync Trabajadores";
                        config.SourceName = "ACME.WorkerService BNV Sync Trabajadores";
                    });
                }
                if (tasks_settings.SAP_Asientos == true)
                {
                    services.AddHostedService<WorkerSAPAsientos>()
                    .Configure<WorkerSAPAsientos>(config =>
                    {
                        config.LogName = "ACME.WorkerService SAP Asientos";
                        config.SourceName = "ACME.WorkerService SAP Asientos";
                    });
                }

                #endregion snippet

            });

        public class TasksSettings
        {
            public bool citas { get; set; }
            public bool sunat_padron { get; set; }
            public bool sunat_pago { get; set; }
            public bool mihistoria { get; set; }
            public bool generar_entregables { get; set; }
            public bool merge_entregables { get; set; }
            public bool parquet_bnv { get; set; }
            public bool SAP_Asientos { get; set; }
        }
    }
}
