using MonksInn.Domain.Enums;
using MonksInn.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace MonksInn.Domain
{
    public class SystemUser : DatabaseObject
    {
        public string Name { get; set; }
        public string EmailAddress { get; set; }
        public string HashedPassword { get; set; }
        public Role Role { get; set; }

        public int Salt { get; set; }
    }
}
