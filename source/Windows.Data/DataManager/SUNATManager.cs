using ACME.Data.Contracts;
using ACME.Data.Entity;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;
using System.Data;
using System;
using Microsoft.Extensions.Logging;
using System.IO;
using System.Net;

namespace ACME.Data.DataManager
{
    public partial class SUNATManager : DbFactoryBase, ISUNATManager
    {
        private readonly IConfiguration _configuration;
        private readonly int iIdUsuario;
        private readonly ILogger<SUNATManager> _logger;

        public SUNATManager(IConfiguration configuration, ILogger<SUNATManager> logger) : base(configuration, "SQLDBConnectionStringSUNAT", logger)
        {
            _configuration = configuration;
            iIdUsuario = int.Parse(@_configuration["WORKER_SUNATPago:IdUsuario"]);
            _logger = logger;
        }

        public async Task<object> RegistraPadron(DataTable dt, int offset, int limit)
        {
            try
            {
                var param = new
                {
                    @tblPadron = dt,
                    @offset,
                    @limit
                };

                var result = await DbExecuteAsync<object>("sproc_SUNAT_RegistraPadron", param, true);
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex.StackTrace);
                throw;
            }
            return true;
        }
    }


}