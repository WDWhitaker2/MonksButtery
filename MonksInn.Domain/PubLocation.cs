using MonksInn.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace MonksInn.Domain
{
    public class PubLocation : DatabaseObject
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public bool DefaultIsDeliveryLocation { get; set; }
        public bool DefaultIsTakeawayLocation { get; set; }

        public virtual List<TappedStockItem> TappedStockItems{get;set;}

    }
}
