using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MonksInn.Backend.Authorization;
using MonksInn.Backend.Models.DeliverySlot;
using MonksInn.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MonksInn.Backend.Controllers
{
    [HasAccess]
    public class DeliverySlotController : BaseController
    {
        public DeliverySlotController(IUnitOfWork uow) : base(uow)
        {
        }
        [HasAccess(SystemPermission.CanAccessDeliverySlotsPage)]
        public IActionResult Index()
        {
            var model = new DeliverySlotViewModel();

            model.DeliverySlots = DeliverySlotLogic.GetAllSlots()
                .OrderBy(a => a.DayOfWeek)
                .ThenBy(a => a.StartTime)
                .ToList();

            return View(model);
        }

        [HttpGet]
        [HasAccess(SystemPermission.CanAddUpdateDeliverySlots)]
        public IActionResult Add()
        {
            var model = new AddViewModel()
            {
            };

            return View(model);
        }

        [HttpPost]
        [HasAccess(SystemPermission.CanAddUpdateDeliverySlots)]
        public IActionResult Add(AddViewModel model)
        {
            ValidateAddModel(model);

            if (ModelState.IsValid)
            {
                DeliverySlotLogic.Add(new Domain.DeliverySlot()
                {
                    DayOfWeek = model.DayOfWeek.Value,
                    StartTime = model.StartTime.Value,
                    EndTime = model.EndTime.Value,
                });

                SaveDbChanges();
                AddAlert("Delivery slot added successfully.");

                return RedirectToAction("index");
            }

            return View(model);
        }


        [HttpGet]
        [HasAccess(SystemPermission.CanAddUpdateDeliverySlots)]
        public IActionResult Update(Guid id)
        {
            ViewBag.IsUpdate = true;

            var slot = DeliverySlotLogic.GetDeliverySlot(id);
            if (slot != null)
            {
                var model = new AddViewModel()
                {
                    Id = slot.Id,
                    DayOfWeek = slot.DayOfWeek,
                    StartTime = slot.StartTime,
                    EndTime = slot.EndTime,
                };

                return View("Add", model);
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        [HasAccess(SystemPermission.CanAddUpdateDeliverySlots)]
        public IActionResult Update(AddViewModel model)
        {
            ValidateAddModel(model);

            if (ModelState.IsValid)
            {
                var slot = DeliverySlotLogic.GetDeliverySlot(model.Id);
                if (slot != null)
                {
                    slot.DayOfWeek = model.DayOfWeek.Value;
                    slot.StartTime = model.StartTime.Value;
                    slot.EndTime = model.EndTime.Value;

                    SaveDbChanges();
                    AddAlert("Delivery slot updated successfully.");
                    return RedirectToAction("index");
                }
            }

            ViewBag.IsUpdate = true;
            return View("Add", model);
        }

        private void ValidateAddModel(AddViewModel model)
        {
            if (model.StartTime.HasValue && model.EndTime.HasValue && model.StartTime >= model.EndTime)
            {
                ModelState.AddModelError("EndTime", "End time must be greater than start time.");
            }
        }
        [HasAccess(SystemPermission.CanArchiveDeliverySlots)]
        public IActionResult Archive(Guid id)
        {

            DeliverySlotLogic.Delete(id);
            SaveDbChanges();
            AddAlert("Delivery slot archived successfully.");
            return RedirectToAction("Index");
        }
    }
}
