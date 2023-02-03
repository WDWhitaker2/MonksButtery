using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MonksInn.Backend.Authorization;
using MonksInn.Backend.Models.StockOrder;
using MonksInn.Domain.Interfaces;
using System;
using System.Linq;

namespace MonksInn.Backend.Controllers
{
    [HasAccess]
    public class StockOrderController : BaseController
    {
        public StockOrderController(IUnitOfWork uow) : base(uow)
        {
        }


        [HasAccess(SystemPermission.CanAccessStockOrdersPage)]
        public IActionResult Index()
        {
            return View(BuildStockOrderViewModel());
        }

        public IActionResult StockOrderTablePartial()
        {
            return PartialView(BuildStockOrderViewModel());
        }

        private StockOrdersViewModel BuildStockOrderViewModel()
        {
            var model = new StockOrdersViewModel();

            model.StockOrders = StockOrderLogic
                .GetAllStockOrders("Beer")
                .OrderBy(a => a.ETA)
                .ThenBy(a => a.Beer.BreweryName)
                .ThenBy(a => a.Beer.BeerName)
                .ToList();

            return model;
        }



        [HttpGet]
        [HasAccess(SystemPermission.CanOrderStock)]
        public IActionResult OrderStockPartial(Guid? beerId)
        {
            var model = new OrderStockPartialViewModel();

            if (beerId.HasValue)
            {
                var beer = BeerLibraryLogic.GetBeer(beerId.Value);
                model.BeerId = beer?.Id;
                model.BreweryName = beer?.BreweryName;
            }

            model.CurrentBreweries = BeerLibraryLogic.GetAllBreweryNames();
            model.CurrentTypes = BeerLibraryLogic.GetAllBeerSubTypes();
            model.CurrentBeers = BeerLibraryLogic.GetAllBeers().ToList().OrderBy(a => a.BreweryName).ThenBy(a => a.BeerName).ToList();
            model.UnitSizeList = StockOrderLogic.GetUnitSizes();

            return PartialView(model);
        }

        [HttpPost]
        [HasAccess(SystemPermission.CanOrderStock)]
        public IActionResult OrderStockPartial(OrderStockPartialViewModel model)
        {

            if (model.AddNewBeer && string.IsNullOrWhiteSpace(model.BeerName))
            {
                ModelState.AddModelError("BeerName", "The Beer Name field is required.");
            }
            if (model.AddNewBeer && string.IsNullOrWhiteSpace(model.BreweryName))
            {
                ModelState.AddModelError("BreweryName", "The Brewery Name field is required.");
            }
            if (!model.AddNewBeer && !model.BeerId.HasValue)
            {
                ModelState.AddModelError("BeerId", "The Beer field is required.");
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

                StockOrderLogic.Add(new Logic.Models.StockOrderLogic.AddLogicModel()
                {
                    BeerId = model.BeerId,
                    CreateWeeklyOrder = model.CreateWeeklyOrder,
                    ETA = model.ETA.Value,
                    Units = model.Units.Value,
                    UnitSize = model.UnitSize
                });

                SaveDbChanges();

                return RedirectToAction("OrderStockPartial");
            }


            model.CurrentBreweries = BeerLibraryLogic.GetAllBreweryNames();
            model.CurrentTypes = BeerLibraryLogic.GetAllBeerSubTypes();
            model.CurrentBeers = BeerLibraryLogic.GetAllBeers().ToList().OrderBy(a => a.BreweryName).ThenBy(a => a.BeerName).ToList();
            model.UnitSizeList = StockOrderLogic.GetUnitSizes();

            return PartialView(model);
        }


        [HttpGet]
        [HasAccess(SystemPermission.CanRecieveStock)]
        public IActionResult ReceiveStockPartial(Guid id)
        {
            var model = new ReceiveStockPartialViewModel();

            var stockorder = StockOrderLogic.GetStockOrder(id, "Beer");

            model.Id = stockorder.Id;
            model.BeerId = stockorder.BeerId;
            model.Units = stockorder.Units;
            model.UnitSize = stockorder.UnitSize;
            model.Strength = stockorder.Beer.Strength;

            model.CurrentBeers = BeerLibraryLogic.GetAllBeers(model.BeerId).Where(a => a.BreweryName == stockorder.Beer.BreweryName).ToList().OrderBy(a => a.BreweryName).ThenBy(a => a.BeerName).ToList();
            model.UnitSizeList = StockOrderLogic.GetUnitSizes();

            return PartialView(model);
        }

        [HttpPost]
        [HasAccess(SystemPermission.CanRecieveStock)]
        public IActionResult ReceiveStockPartial(ReceiveStockPartialViewModel model)
        {
            var stockorder = StockOrderLogic.GetStockOrder(model.Id, "Beer");

            if (ModelState.IsValid)
            {
                StockOrderLogic.ReceiveStock(new Logic.Models.StockOrderLogic.ReceiveStockLogicModel
                {
                    Id = model.Id,
                    BeerId = model.BeerId.Value,
                    BeerStrength = model.Strength.Value,
                    InvoiceNumber = model.InvoiceNumber,
                    UnitPrice = model.UnitPrice.Value,
                    Units = model.Units.Value,
                    UnitSize = model.UnitSize,
                    SellByDate = model.SellByDate.Value

                });
                SaveDbChanges();

                model.ForceCloseModal = true;
            }


            model.CurrentBeers = BeerLibraryLogic.GetAllBeers(model.BeerId).Where(a => a.BreweryName == stockorder.Beer.BreweryName).ToList().OrderBy(a => a.BreweryName).ThenBy(a => a.BeerName).ToList();
            model.UnitSizeList = StockOrderLogic.GetUnitSizes();
            return PartialView(model);
        }

        [HasAccess(SystemPermission.CanArchiveStockOrder)]

        public IActionResult Archive(Guid id)
        {
            StockOrderLogic.Delete(id);
            SaveDbChanges();
            AddAlert("Stock order archived successfully.");

            return RedirectToAction("Index");
        }
    }
}
