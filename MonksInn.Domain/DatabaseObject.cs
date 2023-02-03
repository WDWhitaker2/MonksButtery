using System;
using System.Collections.Generic;
using System.Text;

namespace MonksInn.Domain
{
    public abstract class DatabaseObject
    {
        public Guid Id { get; set; }
        public DateTime DateCreated { get; set; }
        public bool IsArchived { get; set; }
    }
}
