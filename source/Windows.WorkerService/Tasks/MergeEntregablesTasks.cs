using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using ACME.Data.Contracts;
using ACME.Data.Entity;
using ACME.WorkerService.Helpers;
using System;
using System.IO;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Text.Json;
using System.Linq;
using System.Net;
using ACME.WorkerService.Tools;

using System.IO.Pipelines;
using EvoPdf;

namespace ACME.WorkerService.Tasks
{
    internal interface IMergeEntregablesTasks
    {
        Task<object> ListarEpisodiosColaMerge();
        object TestMerge();
    }

    internal class MergeEntregablesTasks : IMergeEntregablesTasks
    {

        private readonly ICitaTrabajadorManager _cita;
        private readonly IConfiguration _configuration;
        private readonly ILogger _logger;
        private readonly ICloudStorage _cloudStorage;
        private readonly IPDFService _pdfservice;
        private readonly IEmailService _emailService;
        private readonly string _logoPath;
        private readonly string _urlApiDocumento;
        private readonly string _fileMerge;
        private readonly int _iIdSubClaseDocumento;
        private readonly string _apionline_url;
        private readonly string _apionline_usr;
        private readonly string _apionline_pwd;
        private readonly string _para_ti;

        public MergeEntregablesTasks(IPDFService pdfservice, ICitaTrabajadorManager cita, ILogger<MergeEntregablesTasks> logger, IConfiguration configuration, ICloudStorage cloudStorage, IEmailService emailService)
        {
            _pdfservice = pdfservice;
            _cita = cita;
            _configuration = configuration;
            _logger = logger;
            _cloudStorage = cloudStorage;
            _emailService = emailService;
            _logoPath = @_configuration["LogoPath"];
            _para_ti = @_configuration["parati"];
            _fileMerge = @_configuration["WORKER_MergeEntregables:FileMerge"];
            _urlApiDocumento = @_configuration["WORKER_GenerarEntregables:UrlApiDocumento"];
            _iIdSubClaseDocumento = int.Parse(_configuration["WORKER_MergeEntregables:iIdSubClaseDocumento"]);
            _apionline_url = @_configuration["WORKER_GenerarEntregables:UrlApiOnline"];
            _apionline_usr = @_configuration["WORKER_GenerarEntregables:user"];
            _apionline_pwd = @_configuration["WORKER_GenerarEntregables:pwd"];

        }

        public object TestMerge()
        {
            IList<string> _files = new List<string>
            {
                @"C:\temp\delete\dhl_pedido.pdf",
                @"C:\temp\delete\Manual de acceso a Middleware BlockChain.pdf",
                @"C:\temp\delete\PAGO_SEDAPAL_SEPTIEMBRE.pdf",
                @"C:\temp\delete\RIPLEY_GIFT_CARD.pdf"
            };

            string destinationFile = $@"C:\temp\delete\merge_{Guid.NewGuid()}.pdf";

            _pdfservice.GenerarMergePdfFile(destinationFile, _files);

            return true;
        }

        public async Task<object> ListarEpisodiosColaMerge()
        {
            try
            {
                

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return false;
            }
        }

        private async Task<object> GenerateMergePdf(string vcCodEpisodioSync, string clase)
        {
            object data = null;

            

            return data;
        }

        private (HttpStatusCode statusCode, string resultContent) SendApiDocument(dynamic body)
        {
            return Helper.Request(body, _urlApiDocumento, null, "post");
        }
        
    }
}
