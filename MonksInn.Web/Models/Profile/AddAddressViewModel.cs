using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MonksInn.Web.Models.Profile
{
    public class AddAddressViewModel
    {
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string City { get; set; }
        public string PostalCode { get; set; }
        public string Country { get; set; }
        public bool SaveSuccessful { get; internal set; }
    }
}
