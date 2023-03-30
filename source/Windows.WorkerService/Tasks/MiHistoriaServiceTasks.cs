using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using ACME.Data.Contracts;
using ACME.WorkerService.Tools;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ACME.WorkerService.Tasks
{
    internal class MiHistoriaServiceTasks : IMiHistoriaServiceTasks
    {

        private readonly IMiHistoriaManager _mihistoria;
        private readonly IConfiguration _configuration;
        private readonly ILogger _logger;

        public MiHistoriaServiceTasks(IMiHistoriaManager mihistoria, ILogger<MiHistoriaProcessingService> logger, IConfiguration configuration)
        {
            _mihistoria = mihistoria;
            _configuration = configuration;
            _logger = logger;
        }

        public async Task<object> RegistraMiHistoriaTipo1()
        {
            await _mihistoria.RegistraMiHistoriaTipo1();

            return true;
        }

        public async Task<object> RegistraMiHistoriaTipo2()
        {
            await _mihistoria.RegistraMiHistoriaTipo2();

            return true;
        }

        public async Task<object> RegistraMiHistoriaTipo3()
        {
            await _mihistoria.RegistraMiHistoriaTipo3();

            return true;
        }



    }
}
