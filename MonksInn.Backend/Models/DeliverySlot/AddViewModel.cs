using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MonksInn.Backend.Models.DeliverySlot
{
    public class AddViewModel
    {
        public Guid Id { get; set; }
        
        [Required]
        [Display(Name ="Day of week")]
        public DayOfWeek? DayOfWeek { get; set; }
        
        [Required]
        [Display(Name = "Start time")]
        public int? StartTime { get; set; }
        
        [Required]
        [Display(Name = "End time")]
        public int? EndTime { get; set; }
    }
}
