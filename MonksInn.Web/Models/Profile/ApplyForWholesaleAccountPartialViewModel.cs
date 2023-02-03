using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MonksInn.Web.Models.Profile
{
    public class ApplyForWholesaleAccountPartialViewModel
    {
        [Required]
        [Display(Name = "Comapany Name")]
        public string ComapanyName { get; set; }
        [Required]
        [Display(Name = "Vat Number")]
        public string VatNumber { get; set; }
        public bool ApplicationSent { get; internal set; }
    }
}
