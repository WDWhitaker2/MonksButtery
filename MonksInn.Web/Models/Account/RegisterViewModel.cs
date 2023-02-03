using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MonksInn.Web.Models.Account
{
    public class RegisterViewModel
    {

        public string Name { get; set; }

        [Required]
        [Display(Name = "Email Address")]
        public string Email { get; set; }


        [Required]
        [Display(Name = "Password")]
        public string ClearPassword { get; set; }
        public string ConfirmPassword { get; set; }
        public bool RegistrationSuccessful { get; internal set; }
    }
}
