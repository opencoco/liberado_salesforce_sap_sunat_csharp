using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Net.Mail;
using Microsoft.Extensions.Configuration;
using ACME.Data.Contracts;
using System.IO;
using Microsoft.Extensions.Logging;
using System.Threading;

namespace ACME.WorkerService.Tools
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _configuration;
        private readonly IMensajePlantillaManager _msgManager;
        private readonly ILogger<EmailService> _logger;

        public EmailService(IConfiguration configuration, IMensajePlantillaManager contratoManager, ILogger<EmailService> logger)
        {
            _configuration = configuration;
            _msgManager = contratoManager;
            _logger = logger;
        }

        public async Task SendEmail(string email, int idPlantilla, Dictionary<string, string> Datos, MemoryStream attachfile, string filename, CancellationToken cancellationToken = default)
        {
            try
            {
                using (var client = new SmtpClient())
                {
                    var credential = new NetworkCredential
                    {
                        UserName = _configuration["EmailSettings:UserName"],
                        Password = _configuration["EmailSettings:Password"]
                    };

                    client.Credentials = credential;
                    client.Host = _configuration["EmailSettings:MailServer"];
                    client.Port = int.Parse(_configuration["EmailSettings:MailPort"]);
                    client.EnableSsl = bool.Parse(_configuration["EmailSettings:EnableSsl"]);

                    //*********Build boddy and subject
                    var msj = await _msgManager.GetByIdAsync(idPlantilla);
                    if (msj == null)
                        throw new Exception($"No existe la plantilla");

                    string subject = msj.VcNombre;
                    string message = msj.VcMensaje;

                    foreach (KeyValuePair<string, string> item in Datos)
                    {
                        message = message.Replace("{" + item.Key + "}", item.Value);
                    }

                    //*****
                    //System.Net.Mime.ContentType ct = new System.Net.Mime.ContentType(System.Net.Mime.MediaTypeNames.Text.Html);

                    using var emailMessage = new MailMessage();
                    emailMessage.To.Add(new MailAddress(email));
                    emailMessage.From = new MailAddress(_configuration["EmailSettings:SenderEmail"]);
                    emailMessage.Subject = subject;
                    emailMessage.IsBodyHtml = true;
                    emailMessage.Body = message;
                    
                    if (attachfile != null) 
                        emailMessage.Attachments.Add(new Attachment(attachfile, filename));

                    client.Send(emailMessage);
                }

                await Task.CompletedTask;
            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Error, ex, "Error when trying to send email.");
            }

        }
    }
}
