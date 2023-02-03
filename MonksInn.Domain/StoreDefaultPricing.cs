using MonksInn.Domain.Enums;
using MonksInn.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace MonksInn.Domain
{
    public class StoreDefaultPricing : DatabaseObject
    {
        public bool IsWholesalePricing { get; set; }


        public string WholeSaleUnitSize { get; set; }
        public decimal? WholeSaleDefaultFlatMarkup { get; set; }


        public BeerType? RetailBeerType { get; set; }
        public decimal? RetailAbvLimit { get; set; }
        public decimal? RetailDefaultAbvPrice { get; set; }

    }
}
