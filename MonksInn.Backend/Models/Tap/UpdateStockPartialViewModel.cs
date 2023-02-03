using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MonksInn.Backend.Models.Tap
{
    public class UpdateStockPartialViewModel
    {
        public string Beer { get; set; }
        public Guid Id { get; set; }
        [Required]
        [Display(Name = "Tap Type")]
        public string TapType { get; set; }
        [Required]
        [Display(Name = "Retail Price")]
        public decimal? RetailPrice { get; set; }

        [Display(Name = "Available For Delivery")]
        public bool ForDelivery { get; set; }

        [Display(Name = "Available For Takeaway")]
        public bool ForTakeaway { get; set; }
       
        [Required]
        [Display(Name = "Pub Location")]
        public Guid PubLocationId { get; set; }

        public bool ForceCloseModal { get; set; }
        public List<string> TapTypeList { get; set; }
        public List<Domain.PubLocation> PubLocationList { get;  set; }
    }
}
