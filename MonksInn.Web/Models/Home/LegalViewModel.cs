using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MonksInn.Web.Models.Home
{
    public class LegalViewModel
    {
        public string TermsAndConditions { get; internal set; }
        public string PrivacyPolicy { get; internal set; }
        public object CookiePolicy { get; internal set; }
    }
}
