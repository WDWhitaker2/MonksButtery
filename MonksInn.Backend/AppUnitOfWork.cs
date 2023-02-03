using MonksInn.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MonksInn.Backend
{
    public class AppUnitOfWork : IUnitOfWork
    {
        public AppUnitOfWork(IDatabaseService databaseService, IEmailService emailService)
        {
            DbContext = databaseService;
            EmailService = emailService;
        }

        public IDatabaseService DbContext { get; set; }
        public IEmailService EmailService { get ; set ; }
    }
}
