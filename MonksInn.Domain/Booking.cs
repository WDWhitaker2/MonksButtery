using MonksInn.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonksInn.Domain
{
    public class Booking : DatabaseObject
    {
        public string FullName { get; set; }
        public string ContactNumber { get; set; }
        public string EmailAddress { get; set; }
        public string Occasion { get; set; }
        public int NumberOfGuests { get; set; }
        public DateTime DateOfBooking { get; set; }
        public int StartTime { get; set; }
        public int EndTime { get; set; }
        public int PrepTime { get; set; }
        public string Comments { get; set; }
        public bool DepositPaid { get; set; }
        public string DepositPaidBy { get; set; }

    }
}

