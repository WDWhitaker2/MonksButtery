using MonksInn.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace MonksInn.Domain
{
    public class SystemUserPasswordResetToken : DatabaseObject
    {
        public Guid SystemUserId { get; set; }
        public virtual SystemUser SystemUser { get; set; }
        public string Hash { get; set; }
        public int Salt { get; set; }
    }
}
