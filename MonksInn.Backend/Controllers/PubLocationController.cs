using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MonksInn.Backend.Authorization;
using MonksInn.Backend.Models.PubLocation;
using MonksInn.Domain.Enums;
using MonksInn.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MonksInn.Backend.Controllers
{
    [HasAccess]
    public class PubLocationController : BaseController
    {
        public PubLocationController(IUnitOfWork uow) : base(uow)
        {
        }
        [HasAccess(SystemPermission.CanAccessPubLocationPage)]
        public IActionResult Index()
        {
            var model = new PubLocationViewModel()
            {
            };
            model.PubLocations = PubLocationLogic.GetAllPubLocations().ToList();
            return View(model);
        }

        [HttpGet]
        [HasAccess(SystemPermission.CanAddUpdatePubLocation)]
        public IActionResult Add()
        {
            var model = new AddViewModel()
            {
            };

            return View(model);
        }

        [HttpPost]
        [HasAccess(SystemPermission.CanAddUpdatePubLocation)]
        public IActionResult Add(AddViewModel model)
        {
            if (ModelState.IsValid)
            {
                PubLocationLogic.Add(new Domain.PubLocation()
                {
                    Address = model.Address,
                    DefaultIsDeliveryLocation = model.DefaultIsDeliveryLocation,
                    DefaultIsTakeawayLocation = model.DefaultIsTakeawayLocation,
                    Name = model.Name
                });

                SaveDbChanges();
                AddAlert("Pub location added successfully.");

                return RedirectToAction("index");
            }

            return View(model);
        }


        [HttpGet]
        [HasAccess(SystemPermission.CanAddUpdatePubLocation)]
        public IActionResult Update(Guid id)
        {
            ViewBag.IsUpdate = true;

            var pub = PubLocationLogic.GetLocation(id);
            if (pub != null)
            {
                var model = new AddViewModel()
                {
                    Id = pub.Id,
                    Address = pub.Address,
                    Name = pub.Name,
                    DefaultIsTakeawayLocation = pub.DefaultIsTakeawayLocation,
                    DefaultIsDeliveryLocation = pub.DefaultIsDeliveryLocation
                };

                return View("Add", model);
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        [HasAccess(SystemPermission.CanAddUpdatePubLocation)]
        public IActionResult Update(AddViewModel model)
        {
            if (ModelState.IsValid)
            {
                var pub = PubLocationLogic.GetLocation(model.Id);
                if (pub != null)
                {

                    pub.Address = model.Address;
                    pub.Name = model.Name;
                    pub.DefaultIsTakeawayLocation = model.DefaultIsTakeawayLocation;
                    pub.DefaultIsDeliveryLocation = model.DefaultIsDeliveryLocation;

                    SaveDbChanges();
                    AddAlert("Pub location updated successfully.");
                    return RedirectToAction("index");
                }
            }

            ViewBag.IsUpdate = true;
            return View("Add", model);
        }

        [HasAccess(SystemPermission.CanArchivePubLocation)]
        public IActionResult Archive(Guid id)
        {

            PubLocationLogic.Delete(id);
            SaveDbChanges();
            AddAlert("Pub location archived successfully.");
            return RedirectToAction("Index");
        }
    }
}
