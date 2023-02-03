using MonksInn.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MonksInn.Web.Models.Profile
{
    public class DeleteAddressPartialViewModel
    {
        public Guid Id { get; set; }
        public StoreUserAddress Address { get;  set; }
        public bool DeleteSuccessful { get;  set; }
    }
}
