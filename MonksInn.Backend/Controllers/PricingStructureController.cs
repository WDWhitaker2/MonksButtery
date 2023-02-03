using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MonksInn.Backend.Authorization;
using MonksInn.Backend.Models.PricingStructure;
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
    public class PricingStructureController : BaseController
    {
        public PricingStructureController(IUnitOfWork uow) : base(uow)
        {
        }

        [HasAccess(SystemPermission.CanAccessPricingStructurePage)]
        public IActionResult Index()
        {
            var allPrices = PricingStructureLogic.GetAllDefaultPrices().ToList();

            var model = new IndexViewModel();


            model.WholesalePrices = allPrices
                .Where(a => a.IsWholesalePricing)
                .OrderBy(a => string.IsNullOrWhiteSpace(a.WholeSaleUnitSize))
                .ThenBy(a => a.WholeSaleUnitSize)
                .ToList();

            model.RetailPrices = allPrices
                .Where(a => !a.IsWholesalePricing)
                .OrderBy(a => !a.RetailBeerType.HasValue)
                .ThenBy(a => a.RetailBeerType)
                .ThenBy(a => a.RetailAbvLimit)
                .ToList();

            return View(model);
        }


        [HttpGet]
        [HasAccess(SystemPermission.CanAddUpdatePricingStructure)]
        public IActionResult AddWholesale()
        {
            var model = new AddWholesaleViewModel();



            model.UnitSizeList = StockOrderLogic.GetUnitSizes();
            return View(model);
        }

        [HttpPost]
        [HasAccess(SystemPermission.CanAddUpdatePricingStructure)]
        public IActionResult AddWholesale(AddWholesaleViewModel model)
        {

            ValidateWholesaleModel(model);
            if (ModelState.IsValid)
            {
                PricingStructureLogic.Add(new Domain.StoreDefaultPricing()
                {
                    IsWholesalePricing = true,
                    WholeSaleDefaultFlatMarkup = model.WholeSaleDefaultFlatMarkup,
                    WholeSaleUnitSize = model.WholeSaleUnitSize,
                });

                SaveDbChanges();
                AddAlert("Pricing structure added successfully.");

                return RedirectToAction("Index");
            }


            model.UnitSizeList = StockOrderLogic.GetUnitSizes();
            return View(model);
        }

        [HttpGet]
        [HasAccess(SystemPermission.CanAddUpdatePricingStructure)]
        public IActionResult UpdateWholesale(Guid id)
        {

            var storePrice = PricingStructureLogic.GetStoreDefaultPrice(id);


            var model = new AddWholesaleViewModel();
            model.Id = storePrice.Id;
            model.WholeSaleDefaultFlatMarkup = storePrice.WholeSaleDefaultFlatMarkup;
            model.WholeSaleUnitSize = storePrice.WholeSaleUnitSize;


            ViewBag.IsUpdate = true;
            model.UnitSizeList = StockOrderLogic.GetUnitSizes();
            return View("AddWholesale", model);
        }

        [HttpPost]
        [HasAccess(SystemPermission.CanAddUpdatePricingStructure)]
        public IActionResult UpdateWholesale(AddWholesaleViewModel model)
        {


            ValidateWholesaleModel(model);

            if (ModelState.IsValid)
            {
                var storePrice = PricingStructureLogic.GetStoreDefaultPrice(model.Id);
                storePrice.WholeSaleDefaultFlatMarkup = model.WholeSaleDefaultFlatMarkup;
                storePrice.WholeSaleUnitSize = model.WholeSaleUnitSize;

                SaveDbChanges();
                AddAlert("Pricing structure updated successfully.");
                return RedirectToAction("Index");
            }

            ViewBag.IsUpdate = true;
            model.UnitSizeList = StockOrderLogic.GetUnitSizes();
            return View("AddWholesale", model);
        }

        private void ValidateWholesaleModel(AddWholesaleViewModel model)
        {
            if (!PricingStructureLogic.CheckWholesaleUnitSizeIsAvailable(model.WholeSaleUnitSize, model.Id))
            {
                ModelState.AddModelError("WholeSaleUnitSize", "A price already exists for this unit size.");
            }
        }

        [HttpGet]
        [HasAccess(SystemPermission.CanAddUpdatePricingStructure)]
        public IActionResult AddRetail()
        {
            var model = new AddRetailViewModel();
            model.BeerTypeList = EnumExtensions.EnumDictionary<BeerType>();

            return View(model);
        }

        [HttpPost]
        [HasAccess(SystemPermission.CanAddUpdatePricingStructure)]
        public IActionResult AddRetail(AddRetailViewModel model)
        {

            ValidateRetailModel(model);
            if (ModelState.IsValid)
            {
                PricingStructureLogic.Add(new Domain.StoreDefaultPricing()
                {
                    IsWholesalePricing = false,
                    RetailAbvLimit = model.RetailAbvLimit.Value,
                    RetailBeerType = model.RetailBeerType,
                    RetailDefaultAbvPrice = model.RetailDefaultAbvPrice,
                });

                SaveDbChanges();
                AddAlert("Pricing structure added successfully.");
                return RedirectToAction("Index");
            }


            model.BeerTypeList = EnumExtensions.EnumDictionary<BeerType>();

            return View(model);
        }

        [HttpGet]
        [HasAccess(SystemPermission.CanAddUpdatePricingStructure)]
        public IActionResult UpdateRetail(Guid id)
        {

            var storePrice = PricingStructureLogic.GetStoreDefaultPrice(id);


            var model = new AddRetailViewModel();
            model.Id = storePrice.Id;
            model.RetailAbvLimit = storePrice.RetailAbvLimit;
            model.RetailBeerType = storePrice.RetailBeerType;
            model.RetailDefaultAbvPrice = storePrice.RetailDefaultAbvPrice;


            ViewBag.IsUpdate = true;
            model.BeerTypeList = EnumExtensions.EnumDictionary<BeerType>();

            return View("AddRetail", model);
        }

        [HttpPost]
        [HasAccess(SystemPermission.CanAddUpdatePricingStructure)]
        public IActionResult UpdateRetail(AddRetailViewModel model)
        {
            ValidateRetailModel(model);

            if (ModelState.IsValid)
            {
                var storePrice = PricingStructureLogic.GetStoreDefaultPrice(model.Id);
                storePrice.RetailAbvLimit = model.RetailAbvLimit.Value;
                storePrice.RetailBeerType = model.RetailBeerType;
                storePrice.RetailDefaultAbvPrice = model.RetailDefaultAbvPrice;

                SaveDbChanges();
                AddAlert("Pricing structure updated successfully.");
                return RedirectToAction("Index");
            }

            ViewBag.IsUpdate = true;
            model.BeerTypeList = EnumExtensions.EnumDictionary<BeerType>();
            return View("AddRetail", model);
        }

        private void ValidateRetailModel(AddRetailViewModel model)
        {
            if (!PricingStructureLogic.CheckRetailOptionIsAvailable(model.RetailBeerType, model.RetailAbvLimit, model.Id))
            {
                ModelState.AddModelError("RetailAbvLimit", "A price already exists for the selected ABV limit and beer type.");
            }
        }

        [HasAccess(SystemPermission.CanArchivePricingStructure)]
        public IActionResult Archive(Guid id)
        {

            PricingStructureLogic.Delete(id);
            SaveDbChanges();

            AddAlert("Pricing structure archived successfully.");
            return RedirectToAction("Index");
        }
    }
}
