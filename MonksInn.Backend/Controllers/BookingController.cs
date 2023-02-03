using Microsoft.AspNetCore.Mvc;
using MonksInn.Backend.Authorization;
using MonksInn.Backend.Models.Bookings;
using MonksInn.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MonksInn.Backend.Controllers
{
    [HasAccess]
    public class BookingController : BaseController
    {
        public BookingController(IUnitOfWork uow) : base(uow)
        {
        }

        [HasAccess(SystemPermission.CanAccessBookingsPage)]
        public IActionResult Index()
        {
            var model = new IndexViewModel();

            model.BookingsList = BookingLogic.GetAllBookings().OrderBy(a => a.DateOfBooking).ThenBy(a => a.StartTime).ToList();

            return View(model);
        }

        [HttpGet]
        [HasAccess(SystemPermission.CanAddUpdateBooking)]
        public IActionResult Add()
        {
            var model = new AddViewModel()
            {
            };

            return View(model);
        }

        [HttpPost]
        [HasAccess(SystemPermission.CanAddUpdateBooking)]
        public IActionResult Add(AddViewModel model)
        {
            var booking = new Domain.Booking()
            {
                Comments = model.Comments,
                ContactNumber = model.ContactNumber,
                DateOfBooking = model.DateOfBooking,
                DepositPaid = model.DepositPaid,
                EmailAddress = model.EmailAddress,
                EndTime = model.EndTime,
                FullName = model.FullName,
                NumberOfGuests = model.NumberOfGuests,
                Occasion = model.Occasion,
                PrepTime = model.PrepTime,
                StartTime = model.StartTime,
            };

            if (BookingLogic.BookingSlotIsTaken(booking))
            {
                ModelState.AddModelError("DateOfBooking", "You cannot make a double booking.");
            }

            if (ModelState.IsValid)
            {
                var newBooking = BookingLogic.Add(booking);


                SaveDbChanges();
                AddAlert("Booking added successfully.");

                return RedirectToAction("index");
            }

            return View(model);
        }


        [HttpGet]
        [HasAccess(SystemPermission.CanAddUpdateBooking)]
        public IActionResult Update(Guid id)
        {
            ViewBag.IsUpdate = true;

            var booking = BookingLogic.GetBooking(id);
            if (booking != null)
            {
                var model = new AddViewModel()
                {
                    Comments = booking.Comments,
                    ContactNumber = booking.ContactNumber,
                    DateOfBooking = booking.DateOfBooking,
                    EmailAddress = booking.EmailAddress,
                    EndTime = booking.EndTime,
                    FullName = booking.FullName,
                    NumberOfGuests = booking.NumberOfGuests,
                    Occasion = booking.Occasion,
                    PrepTime = booking.PrepTime,
                    StartTime = booking.StartTime,
                    DepositPaid = booking.DepositPaid,
                    DepositPaidBy = booking.DepositPaidBy,
            };


                return View("Add", model);
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        [HasAccess(SystemPermission.CanAddUpdateBooking)]
        public IActionResult Update(AddViewModel model)
        {

            if(model.DepositPaid && string.IsNullOrWhiteSpace(model.DepositPaidBy))
            {
                ModelState.AddModelError("DepositPaidBy", "This Field is required when the 'Deposit Paid' field is checked.");
            }
            if (ModelState.IsValid)
            {
                var booking = BookingLogic.GetBooking(model.Id);
                if (booking != null)
                {

                    booking.Comments = model.Comments;
                    booking.ContactNumber = model.ContactNumber;
                    booking.DateOfBooking = model.DateOfBooking;
                    booking.EmailAddress = model.EmailAddress;
                    booking.EndTime = model.EndTime;
                    booking.FullName = model.FullName;
                    booking.NumberOfGuests = model.NumberOfGuests;
                    booking.Occasion = model.Occasion;
                    booking.PrepTime = model.PrepTime;
                    booking.StartTime = model.StartTime;

                    if(!booking.DepositPaid && model.DepositPaid)
                    {
                        BookingLogic.MarkBookingConfirmed(booking, model.DepositPaidBy);
                    }


                    SaveDbChanges();
                    AddAlert("Booking updated successfully.");

                    return RedirectToAction("index");
                }
            }

         
            ViewBag.IsUpdate = true;
            return View("Add", model);
        }



        [HasAccess(SystemPermission.CanArchiveBooking)]
        public IActionResult Archive(Guid id)
        {

            BookingLogic.Delete(id);
            SaveDbChanges();
            AddAlert("Booking archived successfully.");


            return RedirectToAction("Index");
        }


    }
}
