using Microsoft.AspNetCore.Mvc;
using MonksInn.Backend.Authorization;
using MonksInn.Backend.Models.WholesaleApplication;
using MonksInn.Domain;
using MonksInn.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MonksInn.Backend.Controllers
{
    [HasAccess(SystemPermission.CanAccessWholesaleApplicationPage)]
    public class WholesaleApplicationController : BaseController
    {
        public WholesaleApplicationController(IUnitOfWork uow) : base(uow)
        {
        }

        public IActionResult Index()
        {
            var model = new IndexViewModel();
            model.Applications = WholesaleApplicationLogic
                .GetAllApplications("StoreUser")
                .OrderByDescending(a=>a.DateCreated)
                .ToList();

            return View(model);
        }

        [HttpGet]
        public IActionResult ViewApplication(Guid id)
        {
            var model = new RespondPartialViewModel();
            var application = WholesaleApplicationLogic
                .GetWholesaleApplication(id, "StoreUser");

            model.Id = id;
            model.UserName = application.StoreUser.Name;
            model.VatNumber = application.VatNumber;
            model.CompanyName = application.ComapanyName;


            return View(model);
        }

        [HttpPost]
        public IActionResult ViewApplication(RespondPartialViewModel model)
        {
            if (ModelState.IsValid)
            {
                WholesaleApplicationLogic.UpdateApplication(model.Id, model.Result);
                SaveDbChanges();

                return RedirectToAction("Index");
            }

            return View(model);
        }
    }
}
