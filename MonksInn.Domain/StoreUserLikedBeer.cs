using MonksInn.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace MonksInn.Domain
{
    public class StoreUserLikedBeer : DatabaseObject
    {
        public Guid StoreUserId { get; set; }
        public virtual StoreUser StoreUser { get; set; }

        public Guid BeerId { get; set; }
        public virtual Beer Beer { get; set; }

    }
}
