using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MonksInn.Backend.Authorization;
using MonksInn.Backend.Models.Tap;
using MonksInn.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MonksInn.Backend.Controllers
{
    [HasAccess]
    public class TapController : BaseController
    {
        public TapController(IUnitOfWork uow) : base(uow)
        {
        }

        [HasAccess(SystemPermission.CanAccessTappedStockPage)]
        public IActionResult Index()
        {
            return View(BuildTapViewModel());
        }

        public IActionResult TapTablePartial()
        {
            return PartialView(BuildTapViewModel());
        }

        private TapViewModel BuildTapViewModel()
        {
            var model = new TapViewModel();

            model.StockItems = TapLogic
                .GetAllStockItems("Beer", "PubLocation")
                .OrderBy(a => a.PubLocation.Name)
                .ThenBy(a => a.Beer.BeerType)
                .ThenBy(a => a.TapType)
                .ThenBy(a => a.Beer.BreweryName)
                .ThenBy(a => a.Beer.BeerName)
                .ToList();

            return model;
        }

        [HttpGet]
        [HasAccess(SystemPermission.CanUpdateTappedStock)]
        public IActionResult UpdateStockPartial(Guid id)
        {
            var stockitem = TapLogic.GetTappedStockItem(id, "Beer");

            var model = new UpdateStockPartialViewModel()
            {
                Id = id,
                Beer = $"{stockitem.Beer.BreweryName} - {stockitem.Beer.BeerName}",
                RetailPrice = stockitem.RetailPrice,
                TapType = stockitem.TapType,
                ForDelivery = stockitem.ForDelivery,
                ForTakeaway = stockitem.ForTakeaway,
                PubLocationId = stockitem.PubLocationId,
            };

            model.TapTypeList = CellarLogic.TapTypes();
            model.PubLocationList = PubLocationLogic.GetAllPubLocations().ToList();
            return PartialView(model);
        }

        [HttpPost]
        [HasAccess(SystemPermission.CanUpdateTappedStock)]
        public IActionResult UpdateStockPartial(UpdateStockPartialViewModel model)
        {
            if (ModelState.IsValid)
            {
                var stockitem = TapLogic.GetTappedStockItem(model.Id, "Beer");
                stockitem.TapType = model.TapType;
                stockitem.RetailPrice = model.RetailPrice;
                stockitem.ForDelivery = model.ForDelivery;
                stockitem.ForTakeaway = model.ForTakeaway;
                stockitem.PubLocationId = model.PubLocationId;


                SaveDbChanges();
                model.ForceCloseModal = true;
            }

            model.TapTypeList = CellarLogic.TapTypes();
            model.PubLocationList = PubLocationLogic.GetAllPubLocations().ToList();
            return PartialView(model);
        }

        [HasAccess(SystemPermission.CanArchiveTappedStock)]
        public IActionResult Archive(Guid id)
        {

            TapLogic.Delete(id);
            SaveDbChanges();
            AddAlert("Tapped beer archived successfully.");

            return RedirectToAction("Index");
        }
    }
}
