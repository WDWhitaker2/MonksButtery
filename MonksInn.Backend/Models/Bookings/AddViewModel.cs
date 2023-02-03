using MonksInn.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MonksInn.Backend.Models.Bookings
{
    public class AddViewModel
    {
        public Guid Id { get; set; }

        public string FullName { get; set; }
        public string ContactNumber { get; set; }
        public string EmailAddress { get; set; }
        public string Occasion { get; set; }
        public int NumberOfGuests { get; set; }
        public DateTime DateOfBooking { get; set; } = DateTime.Now.Date;
        public int StartTime { get; set; }
        public int EndTime { get; set; }
        public int PrepTime { get; set; }
        public string PaymentType { get; set; }
        public string Comments { get; set; }
        public bool DepositPaid { get; set; }
        public string DepositPaidBy { get; set; }
    }
}
