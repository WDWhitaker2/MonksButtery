using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MonksInn.Web.Models.Account
{
    public class ForgotPasswordViewModel
    {
        [Required]
        [Display(Name = "Email Address")]
        public string EmailAddress { get; set; }
        public bool LinkSent { get; internal set; }
    }
}
