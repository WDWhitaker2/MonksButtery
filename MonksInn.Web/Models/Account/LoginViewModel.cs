using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MonksInn.Web.Models.Account
{
    public class LoginViewModel
    {
        [Required]
        [Display(Name ="Email Address")]
        public string Username { get; set; }
        [Required]
        [Display(Name ="Password")]
        public string ClearPassword { get; set; }
        [Display(Name = "Remember Me")]
        public bool RememberMe { get; set; }
        public bool LoginSuccessful { get; internal set; }
        public bool ShowSendNewLinkButton { get; internal set; }
        public bool ForceSendNewLink { get; set; }
        public bool ForceSendNewLinkSuccess { get; internal set; }
    }
}
