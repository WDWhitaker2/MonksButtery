using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MonksInn.Backend.Models.Account
{
    public class ResetPasswordViewModel
    {
        public string Id { get; set; }
        public bool InvalidLink { get; set; }
        [Display(Name = "New Password")]
        [Required]
        public string ClearPassword { get;  set; }
        public bool PasswordSaved { get; set; }
    }
}
