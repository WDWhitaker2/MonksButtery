using MonksInn.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MonksInn.Web.Models.Store
{
    public class AddToCartViewModel
    {
        public TappedStockItem TappedStockItem { get; internal set; }
        public CellarStockItem CellarStockItem { get; internal set; }


        [Required]
        public int StockUnits { get; set; }
        public Guid? StockItemId { get; set; }
        public bool CloseModal { get; set; }
        public bool IsCellarstock { get; set; }
    }
}
