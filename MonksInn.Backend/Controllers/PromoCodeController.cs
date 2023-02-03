using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MonksInn.Backend.Authorization;
using MonksInn.Backend.Models.PromoCode;
using MonksInn.Domain.Enums;
using MonksInn.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MonksInn.Backend.Controllers
{
    [HasAccess]
    public class PromoCodeController : BaseController
    {
        public PromoCodeController(IUnitOfWork uow) : base(uow)
        {
        }

        [HasAccess(SystemPermission.CanAccessPromoCodesPage)]
        public IActionResult Index()
        {
            var model = new IndexViewModel();

            model.Codes = PromoCodeLogic.GetAllCodes().OrderBy(a => a.Code).ToList();

            return View(model);
        }

        [HttpGet]
        [HasAccess(SystemPermission.CanAddUpdatePromoCodes)]
        public IActionResult Add()
        {
            var model = new AddViewModel();

            return View(model);
        }

        [HttpPost]
        [HasAccess(SystemPermission.CanAddUpdatePromoCodes)]
        public IActionResult Add(AddViewModel model)
        {
            if (ModelState.IsValid)
            {
                PromoCodeLogic.Add(new Domain.PromoCode()
                {
                    Code = model.Code
                });

                SaveDbChanges();

                AddAlert("Promo code added successfully.");
                return RedirectToAction("index");
            }

            return View(model);
        }

        [HttpGet]
        [HasAccess(SystemPermission.CanAddUpdatePromoCodes)]
        public IActionResult Update(Guid id)
        {
            ViewBag.IsUpdate = true;

            var promo = PromoCodeLogic.GetCode(id);
            if (promo != null)
            {
                var model = new AddViewModel()
                {
                    Code = promo.Code,
                    Id = promo.Id
                };

                return View("Add", model);
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        [HasAccess(SystemPermission.CanAddUpdatePromoCodes)]
        public IActionResult Update(AddViewModel model)
        {
            if (ModelState.IsValid)
            {
                var promo = PromoCodeLogic.GetCode(model.Id);
                if (promo != null)
                {
                    promo.Code = model.Code;
                    SaveDbChanges();
                    AddAlert("Promo code updated successfully.");

                    return RedirectToAction("index");
                }
            }
            ViewBag.IsUpdate = true;
            return View("Add", model);
        }

        [HasAccess(SystemPermission.CanArchivePromoCodes)]
        public IActionResult Archive(Guid id)
        {

            PromoCodeLogic.Delete(id);
            SaveDbChanges();
            AddAlert("Promo code archived successfully.");

            return RedirectToAction("Index");
        }
    }

}
