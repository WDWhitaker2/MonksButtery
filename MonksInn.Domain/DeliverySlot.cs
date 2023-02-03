using MonksInn.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace MonksInn.Domain
{
    public class DeliverySlot : DatabaseObject
    {
        public DayOfWeek DayOfWeek { get; set; }
        public int StartTime { get; set; }
        public int EndTime { get; set; }
    }
}
