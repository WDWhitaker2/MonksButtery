using MonksInn.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonksInn.RazorEmailTemplateService.Models
{
    public class WholesaleApplicationResponseModel
    {
        public WholesaleApplication Application { get; internal set; }
        public StoreUser StoreUser { get; internal set; }
    }
}
