using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MonksInn.Backend.Models.Account
{
    public class ForgotPasswordViewModel
    {
        [Display(Name ="Email Address")]
        [Required]
        public string EmailAddress { get; set; }
        public bool LinkSent { get; internal set; }
    }
}
