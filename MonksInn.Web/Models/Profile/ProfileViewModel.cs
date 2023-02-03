using MonksInn.Domain;
using MonksInn.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MonksInn.Web.Models.Profile
{
    public class ProfileViewModel
    {
        [Display(Name = "New Password")]
        public string NewPassword { get; set; }

        [Required]
        public string Name { get; set; }
        [Required]
        [Display(Name = "Email Address")]
        public string EmailAddress { get; set; }
        public bool IsWholeSaleUser { get; internal set; }
        public bool EmailAddressVerified { get; internal set; }
        public List<Order> Orders { get; internal set; }
        public WholesaleApplicationResult? WholesaleApplication { get; internal set; }
    }
}
