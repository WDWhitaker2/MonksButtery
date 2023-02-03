using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MonksInn.Backend.Authorization;
using MonksInn.Backend.Models.SystemUser;
using MonksInn.Domain.Enums;
using MonksInn.Domain.Interfaces;
using MonksInn.Logic.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MonksInn.Backend.Controllers
{
    [HasAccess]
    public class SystemUserController : BaseController
    {
        public SystemUserController(IUnitOfWork uow) : base(uow)
        {
        }

        [HasAccess(SystemPermission.CanAccessSystemUserPage)]
        public IActionResult Index()
        {
            var model = new IndexViewModel()
            {
                Users = SystemUserLogic.GetAllUsers().ToList(),
            };
            return View(model);
        }

        [HttpGet]
        [HasAccess(SystemPermission.CanAddUpdateSystemUser)]
        public IActionResult Add()
        {
            var model = new AddViewModel()
            {
            };

            model.CurrentRoles = EnumExtensions.EnumDictionary<Domain.Enums.Role>();
            return View(model);
        }

        [HttpPost]
        [HasAccess(SystemPermission.CanAddUpdateSystemUser)]
        public IActionResult Add(AddViewModel model)
        {

            ValidateAddModel(model);

            if (ModelState.IsValid)
            {
                var user = new Domain.SystemUser()
                {
                    EmailAddress = model.EmailAddress,
                    Name = model.Name,
                    Role = model.Role,
                };

                if (!string.IsNullOrWhiteSpace(model.NewPassword))
                {
                    SystemUserLogic.SetUserPassword(user, model.NewPassword);
                }

                SystemUserLogic.Add(user);

                SaveDbChanges();

                AddAlert("System user added successfully.");
                return RedirectToAction("index");
            }

            model.CurrentRoles = EnumExtensions.EnumDictionary<Domain.Enums.Role>();
            return View(model);
        }


        [HttpGet]
        [HasAccess(SystemPermission.CanAddUpdateSystemUser)]
        public IActionResult Update(Guid id)
        {
            ViewBag.IsUpdate = true;

            var user = SystemUserLogic.GetUser(id);
            if (user != null)
            {
                var model = new AddViewModel()
                {
                    Id = user.Id,
                    Name = user.Name,
                    Role = user.Role,
                    EmailAddress = user.EmailAddress,
                };

                model.CurrentRoles = EnumExtensions.EnumDictionary<Domain.Enums.Role>();
                return View("Add", model);
            }
            // do notification here
            return RedirectToAction("Index");
        }

        [HttpPost]
        [HasAccess(SystemPermission.CanAddUpdateSystemUser)]
        public IActionResult Update(AddViewModel model)
        {
            ValidateAddModel(model);

            if (ModelState.IsValid)
            {
                var user = SystemUserLogic.GetUser(model.Id);
                if (user != null)
                {

                    user.EmailAddress = model.EmailAddress;
                    user.Name = model.Name;
                    user.Role = model.Role;

                    if (!string.IsNullOrWhiteSpace(model.NewPassword))
                    {
                        SystemUserLogic.SetUserPassword(user, model.NewPassword);
                    }

                    SaveDbChanges();
                    AddAlert("System user updated successfully.");
                    return RedirectToAction("index");
                }
            }

            model.CurrentRoles = EnumExtensions.EnumDictionary<Domain.Enums.Role>();
            ViewBag.IsUpdate = true;
            return View("Add", model);
        }

        [HasAccess(SystemPermission.CanArchiveSystemUser)]
        public IActionResult Archive(Guid id)
        {

            SystemUserLogic.Delete(id);
            SaveDbChanges();
            AddAlert("System user archived successfully.");
            return RedirectToAction("Index");
        }

        private void ValidateAddModel(AddViewModel model)
        {
            if (!SystemUserLogic.CheckEmailIsAvailable(model.EmailAddress, model.Id))
            {
                ModelState.AddModelError("EmailAddress", "Email Address Already Exists.");
            }
        }
    }
}
