using MonksInn.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace MonksInn.Domain
{
    public class OrderItem : DatabaseObject
    {
        public Guid? TappedStockItemId { get; set; }
        public virtual TappedStockItem TappedStockItem { get; set; }
        public int TappedStockUnits { get; set; }
        public decimal TappedStockItemPricePerUnit { get; set; }

        public Guid? CellarStockItemId { get; set; }
        public virtual CellarStockItem CellarStockItem { get; set; }
        public string CellarStockUnitSize { get; set; }
        public decimal CellarStockItemPricePerUnit { get; set; }


        public Guid OrderId { get; set; }
        public virtual Order Order { get; set; }
    }
}
