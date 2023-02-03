using MonksInn.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace MonksInn.Domain
{
    public class StoreUserAddress : DatabaseObject
    {
        public Guid StoreUserId { get; set; }
        public virtual StoreUser StoreUser { get; set; }

        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string City { get; set; }
        public string PostalCode { get; set; }
        public string Country { get; set; }
    }
}
