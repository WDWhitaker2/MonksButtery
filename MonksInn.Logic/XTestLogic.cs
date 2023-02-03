using MonksInn.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace MonksInn.Logic
{
    public class XTestLogic : BaseLogic
    {
        public XTestLogic(IUnitOfWork uow) : base(uow)
        {
        }

        public void SendTestEmail(string toEmail)
        {
            Uow.EmailService.SendTestEmailAsync(toEmail);
        }
    }
}
