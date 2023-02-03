using MonksInn.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace MonksInn.Domain
{
    public class StoreUserRegistrationToken : DatabaseObject
    {
        public string TokenHash { get; set; }
        public Guid StoreUserId { get; set; }
        public virtual StoreUser StoreUser { get; set; }
        public int Salt { get; set; }
    }
}
