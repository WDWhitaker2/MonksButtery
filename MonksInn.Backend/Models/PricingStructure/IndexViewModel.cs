using MonksInn.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MonksInn.Backend.Models.PricingStructure
{
    public class IndexViewModel
    {
        public List<StoreDefaultPricing> WholesalePrices { get;  set; }
        public List<StoreDefaultPricing> RetailPrices { get;  set; }
    }
}
