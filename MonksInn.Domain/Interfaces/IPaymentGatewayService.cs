using System;
using System.Collections.Generic;
using System.Text;

namespace MonksInn.Domain.Interfaces
{
    public interface IPaymentGatewayService
    {
        public void ReceivePayment();
    }
}
