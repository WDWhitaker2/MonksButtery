using MonksInn.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace MonksInn.Domain
{
    public class CartItem : DatabaseObject
    {

        public Guid CartSessionId { get; set; }
        public CartSession CartSession { get; set; }


        public Guid? TappedStockItemId { get; set; }
        public virtual TappedStockItem TappedStockItem { get; set; }
        public int TappedStockUnits { get; set; }
        public decimal? TappedStockItemPricePerUnit { get; set; }


        public bool IsCellarStock { get; set; }


        public Guid? CellarStockItemId { get; set; }
        public virtual CellarStockItem CellarStockItem { get; set; }
        public int CellarStockUnits { get; set; }
        public decimal? CellarStockItemPricePerUnit { get; set; }
    }
}
