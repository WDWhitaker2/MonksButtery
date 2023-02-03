using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MonksInn.Backend.Models.Cellar
{
    public class TapBeerViewModel
    {
        public Guid CellarStockItemId { get; set; }
        [Required]
        [Display(Name = "Tap Type")]
        public string TapType { get; set; }
        [Required]
        [Display(Name = "Pub Location")]
        public Guid PubLocationId { get; set; }




        public bool ForceCloseModal { get; set; }
        
        public List<string> TapTypeList { get; set; }
        public List<Domain.PubLocation> PubLocationList { get; set; }
        
    }
}
