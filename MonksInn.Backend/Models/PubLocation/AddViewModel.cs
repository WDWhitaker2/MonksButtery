using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MonksInn.Backend.Models.PubLocation
{
    public class AddViewModel
    {
        public Guid Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Address { get; set; }
        [Display(Name="Is a Delivery Location")]
        public bool DefaultIsDeliveryLocation { get; set; }
        [Display(Name="Is a Takeaway Location")]
        public bool DefaultIsTakeawayLocation { get; set; }
    }
}
