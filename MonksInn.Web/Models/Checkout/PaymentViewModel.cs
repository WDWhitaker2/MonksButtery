using MonksInn.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MonksInn.Web.Models.Checkout
{
    public class PaymentViewModel
    {
        public Order Order { get; internal set; }
    }
}
