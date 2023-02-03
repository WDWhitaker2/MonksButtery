using MonksInn.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace MonksInn.Domain
{
    public class StoreUser : DatabaseObject
    {

        public string Name { get; set; }
        public string EmailAddress { get; set; }
        public string HashedPassword { get; set; }
        public bool IsWholeSaleUser { get; set; }

        public int Salt { get; set; }


        public Guid? CartSessionID { get; set; }
        public virtual CartSession CartSession { get; set; }
        public bool EmailAddressVerified { get; set; }

    }
}
