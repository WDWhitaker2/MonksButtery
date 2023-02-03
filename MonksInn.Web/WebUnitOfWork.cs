using MonksInn.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MonksInn.Web
{
    public class WebUnitOfWork : IUnitOfWork
    {
        public WebUnitOfWork(IDatabaseService databaseService, IEmailService emailService)
        {
            DbContext = databaseService;
            EmailService = emailService;
            //new SmtpEmailService.SmtpEmailService();
        }

        public IDatabaseService DbContext { get; set; }
        public IEmailService EmailService { get ; set ; }
    }
}
