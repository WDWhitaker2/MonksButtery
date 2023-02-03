using MonksInn.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MonksInn.Backend.Models.Account
{
    public class ProfileViewModel
    {
        public string Name { get; set; }
        public string EmailAddress { get; set; }

        public Role Role { get; set; }


        [Display(Name = "New Password")]
        public string NewPassword { get; set; }
        public bool UpdateSuccessful { get; internal set; }
    }
}
