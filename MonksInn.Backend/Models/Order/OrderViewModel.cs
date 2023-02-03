using MonksInn.Logic.Models.OrderLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MonksInn.Backend.Models.Order
{
    public class OrderViewModel
    {
        public List<Domain.Order> Orders { get; internal set; }
        public string ActiveNav { get; internal set; }
    }
}
