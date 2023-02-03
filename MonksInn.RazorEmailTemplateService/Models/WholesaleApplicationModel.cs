using MonksInn.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonksInn.RazorEmailTemplateService.Models
{
    public class WholesaleApplicationModel
    {
        public WholesaleApplication Application { get; internal set; }
        public StoreUser StoreUser { get; internal set; }
        public EmailButton ReviewAccountButton { get; internal set; }
    }
}
