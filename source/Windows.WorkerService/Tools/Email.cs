using System.Collections.Generic;
using System.IO;
//using System.Net;
//using System.Net.Mail;
//using SendGrid;
//using SendGrid.Helpers.Mail;
using System.Threading.Tasks;
using MimeKit;
using System.Linq;
using System;
using Microsoft.AspNetCore.Http;
using System.Threading;
//using MailKit.Net.Smtp;

namespace ACME.WorkerService.Tools
{
    public class Email
    {
        //static CultureInfo culture;
        private static int portal = -1;

        #region envío de correo

        public static void SendEmail(EmailInfo EmailInfo)
        {
            using (System.Net.Mail.MailMessage mail = new System.Net.Mail.MailMessage())
            {
                mail.From = new System.Net.Mail.MailAddress(EmailInfo.From, EmailInfo.WebMasterName);

                for (int i = 0; i < EmailInfo.To.Length; i++)
                {
                    mail.To.Add(new System.Net.Mail.MailAddress(EmailInfo.To[i]));
                }

                mail.Subject = EmailInfo.Subject;
                mail.Body = EmailInfo.Body;

                if (EmailInfo.Attachments.Count > 0)
                {
                    foreach (var item in EmailInfo.Attachments)
                    {
                        mail.Attachments.Add(new System.Net.Mail.Attachment(item.Value, item.Key));
                    }
                }

                mail.IsBodyHtml = EmailInfo.IsBodyHtml;

                System.Net.Mail.SmtpClient smtp = new System.Net.Mail.SmtpClient
                {
                    Host = EmailInfo.Host,
                    Port = EmailInfo.Port
                };
                //smtp.EnableSsl = EmailInfo.EnableSsl;


                if (EmailInfo.UseDefaultCredentials)
                {
                    System.Net.NetworkCredential networkCredential = new System.Net.NetworkCredential(EmailInfo.UserName, EmailInfo.Password);
                    smtp.UseDefaultCredentials = true;
                    smtp.Credentials = networkCredential;
                }

                smtp.Send(mail);
            }

            //throw new NotImplementedException();
        }

        public class EmailInfo
        {
            public EmailInfo()
            {
                this.Attachments = new Dictionary<string, Stream>();
                //APS.ENT.SISTEMA SISTEMA = new Helper().fvobj_SeleccionarSistema(portal);
                dynamic SISTEMA = new Email().fvobj_SeleccionarSistema(portal);

                this.Host = SISTEMA.SMTP_SERVER;
                this.Port = SISTEMA.SMTP_PUERTO;
                this.From = SISTEMA.WEBMASTER_EMAIL;
                this.WebMasterName = SISTEMA.WEBMASTER_NOMBRE;
                this.UseDefaultCredentials = SISTEMA.ENVIO_EMAIL_AUTENTICADO;
                this.EnableSsl = SISTEMA.ENABLE_SSL;
                this.UserName = SISTEMA.WEBMASTER_USERNAME;
                this.Password = SISTEMA.WEBMASTER_PASSWORD;
                this.IsBodyHtml = true;
            }
            public string Host { get; set; }
            public int Port { get; set; }
            public string[] To { get; set; }
            public string From { get; set; }
            public string Subject { get; set; }
            public string Body { get; set; }
            public bool IsBodyHtml { get; set; }
            public bool EnableSsl { get; set; }
            public bool UseDefaultCredentials { get; set; }
            public string WebMasterName { get; set; }
            public string UserName { get; set; }
            public string Password { get; set; }
            public IDictionary<string, Stream> Attachments { get; set; }
        }

        private dynamic fvobj_SeleccionarSistema(int portal)
        {
            //this.Host = SISTEMA.SMTP_SERVER;
            //this.Port = SISTEMA.SMTP_PUERTO;
            //this.from = SISTEMA.WEBMASTER_EMAIL;
            //this.WebMasterName = SISTEMA.WEBMASTER_NOMBRE;
            //this.UseDefaultCredentials = SISTEMA.ENVIO_EMAIL_AUTENTICADO;
            //this.UserName = SISTEMA.WEBMASTER_USERNAME;
            //this.Password = SISTEMA.WEBMASTER_PASSWORD;

            dynamic SISTEMA = new
            {
                //SMTP_SERVER = "mail.magiadigital.com",
                SMTP_PUERTO = 25,
                //SMTP_SERVER = "chasqui09.nettix.com.mx",
                SMTP_SERVER = "mail.magiadigital.com",
                //SMTP_PUERTO = 465,
                WEBMASTER_EMAIL = "envios@magiadigital.com",
                //WEBMASTER_EMAIL = "mhurtado@magiadigital.com",
                WEBMASTER_NOMBRE = "Webmaster Magiadigital",
                ENVIO_EMAIL_AUTENTICADO = true,
                ENABLE_SSL = false,
                WEBMASTER_USERNAME = "envios",
                WEBMASTER_PASSWORD = "env10s",
            };

            return SISTEMA;
        }



        #endregion

    }

    public class EmailSender : IEmailSender
    {
        private readonly EmailConfiguration _emailConfig;

        public EmailSender(EmailConfiguration emailConfig)
        {
            _emailConfig = emailConfig;
        }

        public void SendEmail(Message message)
        {
            var emailMessage = CreateEmailMessage(message);

            Send(emailMessage);
        }

        private MimeMessage CreateEmailMessage(Message message)
        {
            var emailMessage = new MimeMessage();
            emailMessage.From.Add(new MailboxAddress(_emailConfig.FromName, _emailConfig.FromEmail));
            emailMessage.To.AddRange(message.To);
            emailMessage.Subject = message.Subject;
            //emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Text) { Text = message.Content };
            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html) { Text = string.Format("<h2 style='color:red;'>{0}</h2>", message.Content) };

            return emailMessage;
        }

        private void Send(MimeMessage mailMessage)
        {
            using (var client = new MailKit.Net.Smtp.SmtpClient())
            {
                try
                {
                    client.Connect(_emailConfig.SmtpServer, _emailConfig.Port, _emailConfig.UseSsl);
                    client.AuthenticationMechanisms.Remove("XOAUTH2");
                    client.Authenticate(_emailConfig.UserName, _emailConfig.Password);

                    client.Send(mailMessage);
                }
                catch
                {
                    //log an error message or throw an exception or both.
                    throw;
                }
                finally
                {
                    client.Disconnect(true);
                    client.Dispose();
                }
            }
        }

        public async Task SendEmailAsync(Message message, CancellationToken cancellationToken = default)
        {
            var mailMessage = CreateEmailMessage(message);

            await SendAsync(mailMessage, cancellationToken);
        }

        private async Task SendAsync(MimeMessage mailMessage, CancellationToken cancellationToken)
        {
            using (var client = new MailKit.Net.Smtp.SmtpClient())
            {
                try
                {
                    await client.ConnectAsync(_emailConfig.SmtpServer, _emailConfig.Port, _emailConfig.UseSsl);
                    client.AuthenticationMechanisms.Remove("XOAUTH2");
                    await client.AuthenticateAsync(_emailConfig.UserName, _emailConfig.Password);

                    await client.SendAsync(mailMessage, cancellationToken);
                }
                catch (Exception ex)
                {
                    //log an error message or throw an exception, or both.
                    throw ex;
                }
                finally
                {
                    await client.DisconnectAsync(true);
                    client.Dispose();
                }
            }
        }

    }

    public interface IEmailSender
    {
        void SendEmail(Message message);
        Task SendEmailAsync(Message message, CancellationToken cancellationToken = default);
    }

    public class EmailConfiguration
    {
        public string FromEmail { get; set; }
        public string FromName { get; set; }
        public string SmtpServer { get; set; }
        public int Port { get; set; }
        public bool UseSsl { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
    }

    public class Message
    {
        public List<MailboxAddress> To { get; set; }
        public string Subject { get; set; }
        public string Content { get; set; }

        public IFormFileCollection Attachments { get; set; }

        public Message(IEnumerable<string> to, string subject, string content, IFormFileCollection attachments = null)
        {
            To = new List<MailboxAddress>();

            To.AddRange(to.Select(x => new MailboxAddress(string.Empty,x)));
            Subject = subject;
            Content = content;
            Attachments = attachments;
        }
    }
}
