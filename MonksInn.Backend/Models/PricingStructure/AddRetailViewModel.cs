using MonksInn.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MonksInn.Backend.Models.PricingStructure
{
    public class AddRetailViewModel
    {

        public Guid Id { get; set; }
        [Display(Name ="Beer Type")]
        public BeerType? RetailBeerType { get; set; }
        [Required]
        [Display(Name ="ABV Limit")]
        public decimal? RetailAbvLimit { get; set; }
        [Required]
        [Display(Name ="Default Price")]
        public decimal? RetailDefaultAbvPrice { get; set; }
        public Dictionary<int, string> BeerTypeList { get;  set; }
    }
}
