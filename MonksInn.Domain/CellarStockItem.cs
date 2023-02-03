using MonksInn.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace MonksInn.Domain
{
    public class CellarStockItem : DatabaseObject
    {
        public Guid BeerId { get; set; }
        public virtual Beer Beer { get; set; }
        public decimal CostPrice { get; set; }
        public string UnitSize { get; set; }
        public DateTime? SellByDate { get; set; }

        public string ArchiveReason { get; set; }
        public decimal? WholesalePrice { get; set; }
    }
}
