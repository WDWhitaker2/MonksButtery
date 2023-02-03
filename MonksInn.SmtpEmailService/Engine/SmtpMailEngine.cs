// https://github.com/jstedfast/MailKit



using MimeKit;
using MonksInn.Domain;
using MonksInn.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace MonksInn.SmtpEmailService.Engine
{
    public class SmtpMailEngine
    {
        internal SystemSettings Settings { get; set; }
        internal IDatabaseService DatabaseService { get; }
        internal IEmailTemplateService EmailTemplateService { get; }

        public SmtpMailEngine(IDatabaseService databaseService, IEmailTemplateService emailTemplateService)
        {
            DatabaseService = databaseService;
            EmailTemplateService = emailTemplateService;

            Settings = databaseService.SystemSettings.AsQueryable(true).FirstOrDefault();
        }



        internal void SendEmail(MimeMessage message)
        {
                using (var client = new MailKit.Net.Smtp.SmtpClient())
                {
                    client.Connect(Settings.SmtpServer, Settings.SmtpPort, Settings.SmtpUseSsl);
                    client.Authenticate(Settings.SmtpUsername, Settings.SmtpPassword);
                    client.Send(message);
                    client.Disconnect(true);
                }
            
            
        }


        public MimeMessage GetNewMimeMessage(string bodyText)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("", Settings.DefaultFromEmail));

            message.Body = new TextPart("html")
            {
                Text = bodyText
            };

            return message;
        }
    }
}
