using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MonksInn.Backend.Models.WholesaleApplication
{
    public class RespondPartialViewModel
    {
        public string CompanyName { get; set; }
        public string VatNumber { get; set; }
        public string UserName { get; set; }
        public bool Result { get; set; }
        public Guid Id { get; set; }
    }
}
