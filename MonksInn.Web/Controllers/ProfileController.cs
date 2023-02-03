using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MonksInn.Domain;
using MonksInn.Domain.Interfaces;
using MonksInn.Web.Authorization;
using MonksInn.Web.Models.Profile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MonksInn.Web.Controllers
{
    [HasAccess]
    public class ProfileController : BaseController
    {
        public ProfileController(IUnitOfWork uow) : base(uow)
        {
        }

        [HttpGet]
        public IActionResult Index()
        {
            var user = StoreUserLogic.GetUser(User.GetUserId().Value);

            var model = new ProfileViewModel();
            model.EmailAddress = user.EmailAddress;
            model.Name = user.Name;

            BuildProfileIndexViewModel(model, user);
            return View(model);
        }

        [HttpPost]
        public IActionResult Index(ProfileViewModel model)
        {
            var user = StoreUserLogic.GetUser(User.GetUserId().Value);
            if (!StoreUserLogic.CheckEmailIsAvailable(model.EmailAddress, User.GetUserId().Value))
            {
                ModelState.AddModelError("EmailAddress", "This email Address Already Exists.");
            }

            if (ModelState.IsValid)
            {
                user.Name = model.Name;
                if (user.EmailAddress != model.EmailAddress)
                {
                    user.EmailAddressVerified = false;
                }
                user.EmailAddress = model.EmailAddress;

                if (!string.IsNullOrWhiteSpace(model.NewPassword))
                {
                    StoreUserLogic.SetUserPassword(user, model.NewPassword);
                }

                StoreUserLogic.ResendRegistrationToken(user.EmailAddress);

                SaveDbChanges();
            }

            BuildProfileIndexViewModel(model, user);
            return View(model);
        }

        [HttpGet]
        public IActionResult ApplyForWholesaleAccountPartial()
        {
            return PartialView();
        }

        [HttpPost]
        public IActionResult ApplyForWholesaleAccountPartial(ApplyForWholesaleAccountPartialViewModel model)
        {

            if (ModelState.IsValid)
            {
                StoreUserLogic.ApplyForWholesaleAccount(new WholesaleApplication() {
                    ComapanyName = model.ComapanyName,
                    VatNumber = model.VatNumber,
                    StoreUserId = User.GetUserId().Value,
                });

                SaveDbChanges();

                model.ApplicationSent = true;
            }

            return PartialView(model);

        }


        private void BuildProfileIndexViewModel(ProfileViewModel model, Domain.StoreUser user)
        {
            model.EmailAddressVerified = user.EmailAddressVerified;
            model.IsWholeSaleUser = user.IsWholeSaleUser;
            model.WholesaleApplication = WholesaleApplicationLogic.GetWholesaleApplicationResult(user.Id);
            model.Orders = OrderLogic.GetOrdersForUser(User.GetUserId().Value,
                "Items",
                "PubLocation",
                "Items.TappedStockItem",
                "Items.TappedStockItem.Beer",
                "DeliveryAddress",
                "DeliveryDateAllocation")
                .ToList();
        }

        [HttpGet]
        public IActionResult AddAddressPartial()
        {
            var model = new AddAddressViewModel();
            return PartialView(model);
        }

        [HttpPost]
        public IActionResult AddAddressPartial(AddAddressViewModel model)
        {
            if (ModelState.IsValid)
            {
                StoreUserLogic.AddUserAddress(User.GetUserId().Value, new StoreUserAddress
                {
                    AddressLine1 = model.AddressLine1,
                    AddressLine2 = model.AddressLine2,
                    City = model.City,
                    PostalCode = model.PostalCode,
                    Country = model.Country,
                });
                SaveDbChanges();

                model.SaveSuccessful = true;
            }
            return PartialView(model);
        }

        public IActionResult AddressTablePartial()
        {
            var model = StoreUserLogic.GetUserAddresses(User.GetUserId().Value).ToList();
            return PartialView(model);
        }

        [HttpGet]
        public IActionResult DeleteAddressPartial(Guid id)
        {
            var model = new DeleteAddressPartialViewModel();
            model.Address = StoreUserLogic.GetUserAddress(id);
            model.Id = id;
            return PartialView(model);
        }

        [HttpPost]
        public IActionResult DeleteAddressPartial(DeleteAddressPartialViewModel model)
        {
            StoreUserLogic.DeleteUserAddress(model.Id);
            SaveDbChanges();
            model.DeleteSuccessful = true;

            return PartialView(model);
        }

        public IActionResult OrderHistoryPartial()
        {
            var model = new ProfileViewModel();
            model.Orders = OrderLogic.GetOrdersForUser(User.GetUserId().Value,
                "DeliveryAddress",
                "DeliveryDateAllocation"
                ).ToList();

            return PartialView(model);
        }


        public IActionResult Order(Guid id)
        {
            var model = new OrderViewModel();
            model.Order = OrderLogic
                .GetOrdersForUser(User.GetUserId().Value,
                "Items",
                "Items.TappedStockItem",
                "Items.TappedStockItem.Beer",
                "Items.TappedStockItem.PubLocation",
                "PubLocation",
                "DeliveryAddress",
                "DeliveryDateAllocation",
                "PromoCode")
                .FirstOrDefault(a => a.Id == id);
            return View(model);
        }

        [HttpGet]
        public IActionResult CancelOrder(Guid id)
        {
            OrderLogic.CancelOrder(id);
            SaveDbChanges();
            AddAlert("Order cancelled successfully.");
            return RedirectToAction("Index");
        }


        [HttpGet]
        [AllowAnonymous]
        public IActionResult PrivacyFormPartial()
        {
            return PartialView();
        }

        [HttpPost]
        [AllowAnonymous]
        public IActionResult PrivacyFormPartial(bool AcceptPrivacyCookie)
        {
            SetPrivacyCookie(AcceptPrivacyCookie);
            ViewBag.FormSubmitted = true;
            // set cookie value
            return PartialView();
        }

    }
}
