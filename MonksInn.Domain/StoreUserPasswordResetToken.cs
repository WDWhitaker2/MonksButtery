using MonksInn.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace MonksInn.Domain
{
    public class StoreUserPasswordResetToken:DatabaseObject
    {
          public Guid StoreUserId { get; set; }
        public virtual StoreUser StoreUser { get; set; }
        public string Hash { get; set; }
        public int Salt { get; set; }
    }
}
