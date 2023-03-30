using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ACME.Data.Contracts;
using ACME.Data.DataManager;
using ACME.WorkerService.Tools;
//using Blackmore.Cms.Data.Contracts;
//using Blackmore.Cms.Data.DataManager;
//using Blackmore.Cms.Tools;

namespace ACME.WorkerService
{
    public class RegisterContractMappings : IServiceRegistration
    {
        public void RegisterAppServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<ICitaTrabajadorManager, CitaTrabajadorManager>();
            services.AddTransient<ISUNATManager, SUNATManager>();
            services.AddTransient<IEmailSender, EmailSender>();
        }
    }
}
