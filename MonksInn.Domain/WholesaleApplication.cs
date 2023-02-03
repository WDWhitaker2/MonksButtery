using MonksInn.Domain.Enums;
using MonksInn.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonksInn.Domain
{
    public class WholesaleApplication : DatabaseObject
    {
        public string VatNumber { get; set; }
        public string ComapanyName { get; set; }
        public Guid StoreUserId { get; set; }
        public virtual StoreUser StoreUser { get; set; }
        public WholesaleApplicationResult Result { get; set; }
    }
}
