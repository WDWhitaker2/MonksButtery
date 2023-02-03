using MonksInn.Domain.Enums;
using MonksInn.Domain.Interfaces;
using System;

namespace MonksInn.Domain
{
    public class Beer : DatabaseObject
    {
        public string BeerName { get; set; }
        public string BreweryName { get; set; }
        public decimal? Strength { get; set; }
        public string SubType { get; set; }
        public string Notes { get; set; }
        public BeerType BeerType { get; set; }

        public bool IsSpecialityBeer { get;set; }

        public Guid? ImageId { get; set; }
    }
}
