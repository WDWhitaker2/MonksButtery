using MonksInn.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace MonksInn.Domain
{
    public class PromoCode : DatabaseObject
    {
        public string Code { get; set; }
        public int SpentCount { get; set; }
    }
}
