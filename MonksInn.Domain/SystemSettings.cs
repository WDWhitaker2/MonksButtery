using MonksInn.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Security;
using System.Text;

namespace MonksInn.Domain
{
    public class SystemSettings : DatabaseObject
    {
        // web store legal settings
        public string TermsAndConditions { get; set; }
        public string PrivacyPolicy { get; set; }
        public string CookiePolicy { get; set; }


        //email settings
        public string SmtpServer { get; set; }
        public int SmtpPort { get; set; }
        public bool SmtpUseSsl { get; set; }
        public string DefaultFromEmail { get; set; }
        public string SmtpUsername { get; set; }
        public string SmtpPassword { get; set; }
        public string EmailTemplateBaseWebUrl { get; set; }
        public string EmailTemplateBaseBackendUrl { get; set; }
        public string ContactUsRecipientEmail { get; set; }
    }
}
