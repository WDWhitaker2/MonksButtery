using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MonksInn.Backend.Models.Account
{
    public class LoginViewModel
    {
        [Required]
        public string Email { get; set; }
        [Required]
        [Display(Name ="Password")]
        public string ClearPassword { get; set; }
        [Display(Name = "Remember Me")]
        public bool RememberMe { get; set; }
        public string RedirectTo { get; set; }
    }
}
