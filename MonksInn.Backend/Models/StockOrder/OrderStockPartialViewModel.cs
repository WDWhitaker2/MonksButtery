using MonksInn.Domain;
using MonksInn.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MonksInn.Backend.Models.StockOrder
{
    public class OrderStockPartialViewModel
    {

        [Display(Name = "Unit Size")]
        [Required]
        public string UnitSize { get; set; }
        [Required]
        public int? Units { get; set; }
        [Required]
        public DateTime? ETA { get; set; }

        [Display(Name = "Set as a weekly order")]
        public bool CreateWeeklyOrder { get; set; }

        [Display(Name = "Beer")]
        public Guid? BeerId { get; set; }

        [Display(Name = "Create new beer")]
        public bool AddNewBeer { get; set; }


        [Display(Name = "Beer Name")]
        public string BeerName { get; set; }
        [Display(Name = "Brewery Name")]
        public string BreweryName { get; set; }
        public decimal? Strength { get; set; }
        public string SubType { get; set; }
        public string Notes { get; set; }
        public BeerType BeerType { get; set; }


        public List<string> CurrentTypes { get; set; }
        public List<string> CurrentBreweries { get; set; }
        public List<Beer> CurrentBeers { get; set; }
        public List<string> UnitSizeList { get; set; }

    }
}
