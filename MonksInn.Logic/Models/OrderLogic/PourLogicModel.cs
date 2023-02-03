using MonksInn.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace MonksInn.Logic.Models.OrderLogic
{
    public class PourLogicModel
    {
        public int Size { get; internal set; }
        public OrderItem Item { get; internal set; }
    }
}
