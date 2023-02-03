using MonksInn.Domain;
using MonksInn.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonksInn.Logic
{
    public class BookingLogic : BaseLogic
    {
        public BookingLogic(IUnitOfWork uow) : base(uow)
        {
        }

        public IQueryable<Booking> GetAllBookings(params string[] includes)
        {
            return Uow.DbContext.Bookings.AsQueryable(true, includes);
        }

        public Booking Add(Booking model)
        {
            model.DateCreated = DateTime.Now;

            //if (!string.IsNullOrWhiteSpace(model.Type))
            //    model.Type = model.Type.ToTitleCase();

            return Uow.DbContext.Bookings.Add(model);
        }

        public void Delete(Guid id)
        {
            var booking = Uow.DbContext.Bookings.AsQueryable(true).FirstOrDefault(a => a.Id == id);
            if (booking != null)
            {
                booking.IsArchived = true;
            }
        }

        public Booking GetBooking(Guid id)
        {
            return Uow.DbContext.Bookings.AsQueryable(true).FirstOrDefault(a => a.Id == id);

        }

        public bool BookingSlotIsTaken(Booking booking)
        {

            var results = Uow.DbContext.Bookings.AsQueryable()
                .Where(a => a.DateOfBooking == booking.DateOfBooking)
                .Where(a => (booking.StartTime >= a.StartTime && booking.StartTime < a.EndTime) || (booking.EndTime > a.StartTime && booking.EndTime <= a.EndTime))
                .Any();
            return results;
        }

        public void MarkBookingConfirmed(Booking booking, string depositPaidBy)
        {
            booking.DepositPaid = true;
            booking.DepositPaidBy = depositPaidBy;

            Uow.EmailService.SendBookingConfirmationEmail(booking);

        }
    }
}
