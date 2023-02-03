using MonksInn.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MonksInn.Backend.Models.SystemUser
{
    public class AddViewModel
    {
        [Required]
        public string Name { get; set; }
        [Required]
        [Display(Name= "Email Address")]
        public string EmailAddress { get; set; }
        [Display(Name= "New Password")]
        public string NewPassword { get; set; }

        [Required]
        public Role Role { get; set; }

        public Guid Id { get; set; }
        public Dictionary<int, string> CurrentRoles { get; set; }
    }
}
