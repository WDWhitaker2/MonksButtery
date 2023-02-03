using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MonksInn.Backend.Authorization;
using MonksInn.Backend.Models.StoreUser;
using MonksInn.Domain.Enums;
using MonksInn.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MonksInn.Backend.Controllers
{
    [HasAccess]
    public class StoreUserController : BaseController
    {
        public StoreUserController(IUnitOfWork uow) : base(uow)
        {
        }

        [HasAccess(SystemPermission.CanAccessStoreUserPage)]
        public IActionResult Index()
        {
            var model = new IndexViewModel()
            {
                Users = StoreUserLogic.GetAllUsers().ToList(),
            };
            return View(model);
        }

        [HttpGet]
        [HasAccess(SystemPermission.CanAddUpdateStoreUser)]
        public IActionResult Add()
        {
            var model = new AddViewModel()
            {
            };

            return View(model);
        }

        [HttpPost]
        [HasAccess(SystemPermission.CanAddUpdateStoreUser)]
        public IActionResult Add(AddViewModel model)
        {
            ValidateAddModel(model);

            if (ModelState.IsValid)
            {
                var user = new Domain.StoreUser()
                {
                    EmailAddress = model.EmailAddress,
                    Name = model.Name,
                    IsWholeSaleUser = model.IsWholeSaleUser
                };

                var newuser = StoreUserLogic.Add(user);

                if (model.EmailAddressVerified == true)
                {
                    StoreUserLogic.ForceVerifyEmailLinkForUser(newuser.Id);
                }


                SaveDbChanges();
                AddAlert("Store user added successfully.");

                return RedirectToAction("index");
            }

            return View(model);
        }


        [HttpGet]
        [HasAccess(SystemPermission.CanAddUpdateStoreUser)]
        public IActionResult Update(Guid id)
        {
            ViewBag.IsUpdate = true;

            var user = StoreUserLogic.GetUser(id);
            if (user != null)
            {
                var model = new AddViewModel()
                {
                    Id = user.Id,
                    Name = user.Name,
                    EmailAddress = user.EmailAddress,
                    IsWholeSaleUser = user.IsWholeSaleUser,
                    EmailAddressVerified = user.EmailAddressVerified,
                };

                return View("Add", model);
            }
            // do notification here
            return RedirectToAction("Index");
        }

        [HttpPost]
        [HasAccess(SystemPermission.CanAddUpdateStoreUser)]
        public IActionResult Update(AddViewModel model)
        {
            ValidateAddModel(model);

            if (ModelState.IsValid)
            {
                var user = StoreUserLogic.GetUser(model.Id);
                if (user != null)
                {

                    user.EmailAddress = model.EmailAddress;
                    user.Name = model.Name;
                    user.IsWholeSaleUser = model.IsWholeSaleUser;

                    if (!user.EmailAddressVerified && model.EmailAddressVerified == true)
                    {
                        StoreUserLogic.ForceVerifyEmailLinkForUser(user.Id);
                    }

                    SaveDbChanges();
                    AddAlert("Store user updated successfully.");
                    return RedirectToAction("index");
                }
            }

            ViewBag.IsUpdate = true;
            return View("Add", model);
        }

        [HasAccess(SystemPermission.CanArchiveStoreUser)]
        public IActionResult Archive(Guid id)
        {
            StoreUserLogic.Delete(id);
            SaveDbChanges();
            AddAlert("Store user archived successfully.");
            return RedirectToAction("Index");
        }
        private void ValidateAddModel(AddViewModel model)
        {
            if (!StoreUserLogic.CheckEmailIsAvailable(model.EmailAddress, model.Id))
            {
                ModelState.AddModelError("EmailAddress", "Email Address Already Exists.");
            }
        }
    }
}
