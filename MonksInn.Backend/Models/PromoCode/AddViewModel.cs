using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MonksInn.Backend.Models.PromoCode
{
    public class AddViewModel
    {
        [Required]
        public string Code { get; set; }
        public Guid Id { get; set; }
    }
}
