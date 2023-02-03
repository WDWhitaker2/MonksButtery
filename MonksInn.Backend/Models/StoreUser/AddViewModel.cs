using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MonksInn.Backend.Models.StoreUser
{
    public class AddViewModel
    {
        [Required]
        public string Name { get; set; }
        [Required]
        [Display(Name = "Email Address")]
        public string EmailAddress { get; set; }


        public Guid Id { get; set; }
        public bool IsWholeSaleUser { get;  set; }
        public bool EmailAddressVerified { get; set; }
    }
}
