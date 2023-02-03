using MonksInn.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MonksInn.Backend.Models.PricingStructure
{
    public class AddWholesaleViewModel
    {
        public Guid Id { get; set; }


        [Display(Name = "Unit Size")]
        public string WholeSaleUnitSize { get; set; }
        [Required]          
        [Display(Name = "Flat Markup Amount")]
        public decimal? WholeSaleDefaultFlatMarkup { get; set; }


        //public BeerType RetailBeerType { get; set; }
        //public decimal? RetailAbvLimit { get; set; }
        //public decimal? RetailDefaultAbvPrice { get; set; }


        public List<string> UnitSizeList { get; set; }
    }
}
