using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MonksInn.Backend.Authorization;
using MonksInn.Backend.Models.Cellar;
using MonksInn.Domain;
using MonksInn.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MonksInn.Backend.Controllers
{
    [HasAccess]
    public class CellarController : BaseController
    {
        public CellarController(IUnitOfWork uow) : base(uow)
        {
        }
        [HasAccess(SystemPermission.CanAccessCellarStockPage)]
        public IActionResult Index()
        {
            return View(BuildCellarViewModel());
        }

        public IActionResult CellarTablePartial()
        {
            return PartialView(BuildCellarViewModel());
        }

        private CellarViewModel BuildCellarViewModel()
        {
            var model = new CellarViewModel();

            model.StockItems = CellarLogic
                .GetAllStockItems("Beer")
                .OrderBy(a => a.SellByDate)
                .ThenBy(a => a.Beer.BreweryName)
                .ThenBy(a => a.Beer.BeerName)
                .ToList();

            return model;
        }

        [HttpGet]
        public IActionResult TapBeerPartial(Guid Id)
        {
            var model = new TapBeerViewModel()
            {
                CellarStockItemId = Id,
                TapTypeList = CellarLogic.TapTypes(),
                PubLocationList = PubLocationLogic.GetAllPubLocations().ToList(),
            };
            return PartialView(model);
        }

        [HttpPost]
        public IActionResult TapBeerPartial(TapBeerViewModel model)
        {
            if (ModelState.IsValid)
            {
                CellarLogic.TapBeer(model.CellarStockItemId, model.TapType, model.PubLocationId);
                SaveDbChanges();
                model.ForceCloseModal = true;
            }
            model.TapTypeList = CellarLogic.TapTypes();
            model.PubLocationList = PubLocationLogic.GetAllPubLocations().ToList();

            return PartialView(model);
        }

        public IActionResult GetWholesalePrice(GetWholesalePriceViewModel model)
        {
            var result = 0m;

            result = StockOrderLogic.GetDefaultWholesalePriceForPrice( model.UnitSize,model.Price);


            return Json(result);
        }

        [HttpGet]
        public IActionResult AddStockPartial()
        {
            var model = new UpdateStockPartialViewModel();
            model.CurrentBreweries = BeerLibraryLogic.GetAllBreweryNames();
            model.CurrentTypes = BeerLibraryLogic.GetAllBeerSubTypes();
            model.UnitSizeList = StockOrderLogic.GetUnitSizes();
            model.CurrentBeers = BeerLibraryLogic.GetAllBeers().ToList().OrderBy(a => a.BreweryName).ThenBy(a => a.BeerName).ToList();
            model.IsAddView = true;

            return PartialView("UpdateStockPartial", model);
        }

        [HttpPost]
        public IActionResult AddStockPartial(UpdateStockPartialViewModel model)
        {
            if (model.AddNewBeer)
            {
                if (string.IsNullOrWhiteSpace(model.BeerName))
                    ModelState.AddModelError("BeerName", "Beer Name is a required field.");

                if (string.IsNullOrWhiteSpace(model.BreweryName))
                    ModelState.AddModelError("BreweryName", "Brewery Name is a required field.");
            }
            else
            {
                if (!model.BeerId.HasValue)
                    ModelState.AddModelError("BeerId", "Beer is a required field.");
            }

            if (ModelState.IsValid)
            {

                if (model.AddNewBeer)
                {
                    var beer = BeerLibraryLogic.Add(new Domain.Beer()
                    {
                        BeerName = model.BeerName,
                        BreweryName = model.BreweryName,
                        BeerType = model.BeerType,
                        Notes = model.Notes,
                        Strength = model.Strength,
                        SubType = model.SubType,
                    });

                    model.BeerId = beer.Id;
                }


                var stockitem = new CellarStockItem();


                stockitem.BeerId = model.BeerId.Value;
                stockitem.CostPrice = model.CostPrice;
                stockitem.SellByDate = model.SellByDate;
                stockitem.UnitSize = model.UnitSize;
                stockitem.WholesalePrice = model.WholesalePrice;

                CellarLogic.AddStockItem(stockitem);


                SaveDbChanges();
                model.ForceCloseModal = true;
            }
            model.IsAddView = true;
            model.CurrentBreweries = BeerLibraryLogic.GetAllBreweryNames();
            model.CurrentTypes = BeerLibraryLogic.GetAllBeerSubTypes();
            model.UnitSizeList = StockOrderLogic.GetUnitSizes();
            model.CurrentBeers = BeerLibraryLogic.GetAllBeers().ToList().OrderBy(a => a.BreweryName).ThenBy(a => a.BeerName).ToList();
            return PartialView("UpdateStockPartial", model);
        }


        [HttpGet]
        public IActionResult UpdateStockPartial(Guid id)
        {
            var stockitem = CellarLogic.GetCellarStockItem(id, "Beer");

            var model = new UpdateStockPartialViewModel()
            {
                Id = id,
                Beer = $"{stockitem.Beer.BreweryName} - {stockitem.Beer.BeerName}",
                CostPrice = stockitem.CostPrice,
                SellByDate = stockitem.SellByDate,
                UnitSize = stockitem.UnitSize,
                WholesalePrice = stockitem.WholesalePrice,
            };


            model.UnitSizeList = StockOrderLogic.GetUnitSizes();
            return PartialView(model);
        }

        [HttpPost]
        public IActionResult UpdateStockPartial(UpdateStockPartialViewModel model)
        {
            if (ModelState.IsValid)
            {
                var stockitem = CellarLogic.GetCellarStockItem(model.Id, "Beer");
                stockitem.CostPrice = model.CostPrice;
                stockitem.SellByDate = model.SellByDate;
                stockitem.UnitSize = model.UnitSize;
                stockitem.WholesalePrice = model.WholesalePrice;

                CellarLogic.UpdateCellarStockOrderValidation(stockitem);

                SaveDbChanges();
                model.ForceCloseModal = true;
            }

            model.UnitSizeList = StockOrderLogic.GetUnitSizes();
            return PartialView(model);
        }

        [HttpGet]
        public IActionResult RemoveStockPartial(Guid id)
        {
            var stockitem = CellarLogic.GetCellarStockItem(id, "Beer");

            var model = new RemoveStockPartialViewModel()
            {
                Id = id,
                Beer = $"{stockitem.Beer.BreweryName} - {stockitem.Beer.BeerName}",
                UnitSize = stockitem.UnitSize
            };
            return PartialView(model);
        }

        [HttpPost]
        public IActionResult RemoveStockPartial(RemoveStockPartialViewModel model)
        {
            if (ModelState.IsValid)
            {
                CellarLogic.Delete(model.Id, model.Reason);
                SaveDbChanges();
                model.ForceCloseModal = true;
            }

            return PartialView(model);
        }

    }
}
