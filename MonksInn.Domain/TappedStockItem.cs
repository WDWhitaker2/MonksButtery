using MonksInn.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace MonksInn.Domain
{
    public class TappedStockItem : DatabaseObject
    {
        public Guid BeerId { get; set; }
        public virtual Beer Beer { get; set; }
        public string UnitSize { get; set; }
        public string TapType { get; set; }
        public decimal? RetailPrice{ get; set; }

        public Guid PubLocationId { get; set; }
        public virtual PubLocation PubLocation { get; set; }

        public bool ForDelivery { get; set; }
        public bool ForTakeaway { get; set; }

    }
}
