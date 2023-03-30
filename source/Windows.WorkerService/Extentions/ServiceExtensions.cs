using ACME.Data.Contracts;
using ACME.Data.DataManager;
using ACME.WorkerService.Tools;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
//using SendGrid;
using System;
using ACME.WorkerService.Tasks;

namespace ACME.WorkerService
{
    public static class ServiceExtensions
    {
        public static void ConfigureEmailService(this IServiceCollection services, IConfiguration configuration)
        {
            var emailConfig = configuration.GetSection("EmailSettings").Get<EmailSettings>();
            services.AddSingleton(emailConfig);
            //services.AddTransient<IEmailSender, EmailSender>();
            services.AddScoped<IEmailService, EmailService>();
        }

        public static void ConfigureRepositoryWrapper(this IServiceCollection services)
        {
            services.AddScoped<IMensajePlantillaManager, MensajePlantillaManager>();
            //services.AddTransient<IEmailSender, EmailSender>();

            services.AddScoped<IMiHistoriaManager, MiHistoriaManager>();
            services.AddScoped<IMiHistoriaServiceTasks, MiHistoriaServiceTasks>();
            services.AddScoped<IMiHistoriaProcessingService, MiHistoriaProcessingService>();

            services.AddScoped<ICitaTrabajadorManager, CitaTrabajadorManager>();
            services.AddScoped<ICitaDataServiceTasks, CitaDataServiceTasks>();
            services.AddScoped<ICitaProcessingService, CitaDataProcessingService>();
            services.AddScoped<IBNVManager, BNVManager>();
            services.AddScoped<ISUNATManager, SUNATManager>();
            services.AddScoped<ISUNATPagoManager, SUNATPagoManager>();
            services.AddScoped<ISUNATServiceTasks, SUNATServiceTasks>();
            services.AddScoped<ISalesForceTasks, SalesForceTasks>();
            services.AddScoped<ISUNATPagoTasks, SUNATPagoTasks>();
            services.AddScoped<ISUNATProcessingService, SUNATProcessingService>();
            services.AddScoped<ISUNATPagoService, SUNATPagoService>();
            services.AddScoped<ISalesForceService, SalesForceService>();
            services.AddScoped<IGenerarEntregablesTasks, GenerarEntregablesTasks>();
            services.AddScoped<IGenerarEntregablesService, GenerarEntregablesService>();
            services.AddScoped<ISAPAsientosService, SAPAsientosService>();
            services.AddScoped<ISAPAsientosTasks, SAPAsientosTasks>();
            services.AddScoped<IMergeEntregablesTasks, MergeEntregablesTasks>();
            services.AddScoped<IMergeEntregablesService, MergeEntregablesService>();
        }

    }

}
