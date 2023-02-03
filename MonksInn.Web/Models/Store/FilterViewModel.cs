using MonksInn.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MonksInn.Web.Models.Store
{
    public class FilterViewModel
    {
        public FilterViewModel()
        {
            Breweries = new List<string>();
            UnitSizes = new List<string>();
            Locations = new List<string>();
            BeerTypes = new List<string>();
            Subtypes = new List<string>();
            Availability = new List<string>();
            UserFavorites = new List<Guid>();
        }
        public List<string> BreweryList { get; internal set; }
        public List<string> UnitSizeList { get; internal set; }
        public List<string> BeerTypeList { get; internal set; }
        public List<string> SubtypeList { get; internal set; }
        public List<string> LocationList { get; internal set; }
        public List<string> AvailabilityList { get; internal set; }
        public List<string> TakeawayLocationLists { get; internal set; }
        public List<Guid> UserFavorites { get; internal set; }
        
        public bool CartHasItems { get; internal set; }
        public bool HasFilters { get; internal set; }
        public bool IsWholeSaleUser { get; internal set; }
        
        public string Search { get; set; }
        public List<string> Breweries { get; set; }
        public List<string> UnitSizes { get; set; }
        public List<string> Locations { get; set; }
        public List<string> BeerTypes { get; set; }
        public List<string> Subtypes { get; set; }
        public List<string> Availability { get; set; }


        public List<TappedStockItem> TappedStock { get; internal set; }
        public List<CellarStockItem> CellarStock { get; internal set; }

    }
}
