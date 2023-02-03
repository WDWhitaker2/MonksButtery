using MonksInn.Domain;
using MonksInn.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MonksInn.Backend.Models.Cellar
{
    public class UpdateStockPartialViewModel
    {
          
        public string Beer { get; set; }
        [Display(Name = "Cost Price")]
        [Required]
        public decimal CostPrice { get; set; }
        [Display(Name = "Wholesale Price")]
        public decimal? WholesalePrice { get; set; }
        [Display(Name = "Unit Size")]
        [Required]
        public string UnitSize { get; set; }
      
        [Display(Name = "Sell By Date")]
        public DateTime? SellByDate { get; set; } = DateTime.Now.Date.AddMonths(6);
        public Guid Id { get;  set; }
        public bool ForceCloseModal { get; set; }
        public List<string> UnitSizeList { get; set; }
        public List<Beer> CurrentBeers { get; internal set; }
        
        public bool IsAddView { get; set; }
        public Guid? BeerId { get; set; }

        public bool AddNewBeer { get; set; }
        public string BeerName { get; set; }
        public string BreweryName { get; set; }
        public BeerType BeerType { get; set; }
        public string Notes { get; set; }
        public decimal? Strength { get; set; }
        public string SubType { get; set; }
        public List<string> CurrentTypes { get; internal set; }
        public List<string> CurrentBreweries { get; internal set; }
    }
}
