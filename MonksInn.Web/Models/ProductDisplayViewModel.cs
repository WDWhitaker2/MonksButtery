using MonksInn.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MonksInn.Web.Models
{
    public class ProductDisplayViewModel
    {
        public CellarStockItem CellarStockItem { get; set; }
        public TappedStockItem TappedStockItem { get; set; }

        public bool ShowStoreButtons { get; set; } = true;
        public bool ProductIsLiked { get; set; }
        public bool ShowTapTypeInsteadOfDeliveryOption { get; set; }
        public bool IsShort { get; set; }
    }
}
