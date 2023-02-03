using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MonksInn.Backend.Models.SystemSettings
{
    public class SystemSettingsViewModel
    {
        [Display(Name = "Terms And Conditions")]
        public string TermsAndConditions { get; set; }

        [Display(Name = "Privacy Policy")]
        public string PrivacyPolicy { get; set; }
     
        [Display(Name = "Cookie Policy")]
        public string CookiePolicy { get; set; }

        [Required]
        [Display(Name = "Smtp Server")]
        public string SmtpServer { get; set; }
        
        [Required]
        [Display(Name = "Smtp Port")]
        public int SmtpPort { get; set; }
        
        [Required]
        [Display(Name = "Smtp Uses SSL")]
        public bool SmtpUseSsl { get; set; }
        
        [Required]
        [Display(Name = "Default From Email")]
        public string DefaultFromEmail { get; set; }
        
        [Required]
        [Display(Name = "Smtp Username")]
        public string SmtpUsername { get; set; }
        
        [Required]
        [Display(Name = "Smtp Password")] 
        public string SmtpPassword { get; set; }
        
        [Required]
        [Display(Name = "Email Template Base Web Url")] 
        public string EmailTemplateBaseWebUrl { get; set; }
       
        [Required]
        [Display(Name = "Email Template Base Backend Url")] 
        public string EmailTemplateBaseBackendUrl { get; set; }

        [Required]
        [Display(Name = "Contact Us Recipient Email")] 
        public string ContactUsRecipientEmail { get; set; }
    }

}
