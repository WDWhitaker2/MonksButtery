using System;
using System.Collections.Generic;
using System.Text;

namespace MonksInn.Domain.Interfaces
{
    public interface IUnitOfWork
    {
        public IDatabaseService DbContext { get; set; }
        public IEmailService EmailService { get; set; }
        //public IPaymentGatewayService PaymentGateway { get; set; }
    }
}
