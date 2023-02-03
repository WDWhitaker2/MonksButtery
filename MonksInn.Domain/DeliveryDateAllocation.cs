using MonksInn.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace MonksInn.Domain
{
    public class DeliveryDateAllocation : DatabaseObject
    {
        public DateTime DateAllocation { get; set; }
        public DayOfWeek DayOfWeek { get; set; }
        public int CommitedStartHour { get; set; }
        public int CommitedEndHour { get; set; }

        public virtual List<Order> Orders { get; set; }
    }
}
