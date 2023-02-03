using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using MonksInn.Domain.Interfaces;
using MonksInn.Logic.Extensions;
using MonksInn.Web.Authorization;
using MonksInn.Web.Models.Store;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MonksInn.Web.Controllers
{
    public class StoreController : BaseController
    {
        public StoreController(IUnitOfWork uow) : base(uow)
        {
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            ViewBag.UseFullPageBeerBackground = true;
            base.OnActionExecuting(context);
        }

        public IActionResult Index(FilterViewModel model)
        {
            model = BuildIndexModal(model);
            return View(model);
        }

        public IActionResult ResultsPartial(FilterViewModel model)
        {
            model = BuildIndexModal(model);
            return PartialView(model);
        }

        public IActionResult FilterFormPartial(FilterViewModel model)
        {
            model = BuildIndexModal(model);
            return PartialView(model);
        }


        private FilterViewModel BuildIndexModal(FilterViewModel model)
        {
            model.IsWholeSaleUser = GetCurrentUser()?.IsWholeSaleUser ?? false;

            var cellarStock = CellarLogic
                .GetAvailableStockItems(GetCartSessionCookie(), "Beer")
                .Where(a => a.WholesalePrice.HasValue)
                .OrderBy(a => a.Beer.BreweryName)
                .ThenBy(a => a.Beer.BeerName)
                .ToList();

            var tappedStock = TapLogic
                .GetAllStockItems("Beer", "PubLocation")
                .Where(a => a.RetailPrice.HasValue)
                .OrderBy(a => a.Beer.BeerName)
                .ToList();

            // we generate filter options first while we have a full dataset
            if (model.IsWholeSaleUser)
            {
                model.BreweryList = cellarStock.Select(a => a.Beer.BreweryName).Distinct().OrderBy(a => a).ToList();
                model.UnitSizeList = cellarStock.Select(a => a.UnitSize).Distinct().OrderBy(a => a).ToList();
                model.BeerTypeList = cellarStock.Select(a => a.Beer.BeerType.GetDisplayName()).Distinct().OrderBy(a => a).ToList();
                model.SubtypeList = cellarStock.Select(a => a.Beer.SubType).Distinct().OrderBy(a => a).ToList();
            }
            else
            {
                model.BreweryList = tappedStock.Select(a => a.Beer.BreweryName).Distinct().OrderBy(a => a).ToList();
                model.LocationList = tappedStock.Select(a => a.PubLocation.Name).Distinct().OrderBy(a => a).ToList();
                model.BeerTypeList = tappedStock.Select(a => a.Beer.BeerType.GetDisplayName()).Distinct().OrderBy(a => a).ToList();
                model.SubtypeList = tappedStock.Select(a => a.Beer.SubType).Distinct().OrderBy(a => a).ToList();
                model.AvailabilityList = new List<string>() { "Delivery", "Takeaway" };
                model.TakeawayLocationLists = tappedStock.Where(a => a.ForTakeaway).Select(a => a.PubLocation.Name).Distinct().OrderBy(a => a).ToList();
            }


            // filter search string
            if (!string.IsNullOrWhiteSpace(model.Search))
            {
                model.HasFilters = true;

                var searchterms = model.Search.ToLower().Split(' ');
                cellarStock = cellarStock
                    .Where(a => searchterms
                        .Any(b => (!string.IsNullOrWhiteSpace(a.Beer.BeerName) && a.Beer.BeerName.Contains(b, StringComparison.OrdinalIgnoreCase))
                           || (!string.IsNullOrWhiteSpace(a.Beer.Notes) && a.Beer.Notes.Contains(b, StringComparison.OrdinalIgnoreCase))
                        ))
                    .ToList();


                tappedStock = tappedStock
                    .Where(a => searchterms
                        .Any(b => (!string.IsNullOrWhiteSpace(a.Beer.BeerName) && a.Beer.BeerName.Contains(b, StringComparison.OrdinalIgnoreCase))
                           || (!string.IsNullOrWhiteSpace(a.Beer.Notes) && a.Beer.Notes.Contains(b, StringComparison.OrdinalIgnoreCase))
                        ))
                    .ToList();
            }

            // filter breweries
            if (model.Breweries.Any())
            {
                model.HasFilters = true;
                cellarStock = cellarStock.Where(a => model.Breweries.Contains(a.Beer.BreweryName)).ToList();
                tappedStock = tappedStock.Where(a => model.Breweries.Contains(a.Beer.BreweryName)).ToList();
            }

            // filter locations
            if (model.Locations.Any())
            {
                model.HasFilters = true;
                tappedStock = tappedStock.Where(a => model.Locations.Contains(a.PubLocation.Name)).ToList();
            }

            //filter availability
            if (model.Availability.Any())
            {
                model.HasFilters = true;
                var forDelivery = model.Availability.Contains("Delivery");
                var forTakeaway = model.Availability.Contains("Takeaway");
                tappedStock = tappedStock.Where(a => (forDelivery && a.ForDelivery) || (forTakeaway && a.ForTakeaway)).ToList();
            }

            // filter UnitSizes
            if (model.UnitSizes.Any())
            {
                model.HasFilters = true;
                cellarStock = cellarStock.Where(a => model.UnitSizes.Contains(a.UnitSize)).ToList();
            }

            // filter BeerTypes
            if (model.BeerTypes.Any())
            {
                model.HasFilters = true;
                cellarStock = cellarStock.Where(a => model.BeerTypes.Contains(a.Beer.BeerType.GetDisplayName())).ToList();
                tappedStock = tappedStock.Where(a => model.BeerTypes.Contains(a.Beer.BeerType.GetDisplayName())).ToList();
            }

            // filter BeerTypes
            if (model.Subtypes.Any())
            {
                model.HasFilters = true;
                cellarStock = cellarStock.Where(a => model.Subtypes.Contains(a.Beer.SubType)).ToList();
                tappedStock = tappedStock.Where(a => model.Subtypes.Contains(a.Beer.SubType)).ToList();
            }


            model.CellarStock = cellarStock.ToList();
            model.TappedStock = tappedStock.ToList();


            model.CartHasItems = CartSessionLogic.CartHasItem(GetCartSessionCookie());

            if (User.IsAuthenticated())
            {
                model.UserFavorites = StoreUserLogic.GetUserLikedBeers(User.GetUserId().Value).Select(a => a.BeerId).ToList();
            }

            return model;
        }


        [HttpGet]
        [HasAccess]
        public void AddLikedBeer(Guid beerId)
        {
            StoreUserLogic.AddLikedBeer(User.GetUserId().Value, beerId);
            SaveDbChanges();
        }
        [HttpGet]
        [HasAccess]
        public void RemoveLikedBeer(Guid beerId)
        {
            StoreUserLogic.RemoveLikedBeer(User.GetUserId().Value, beerId);
            SaveDbChanges();
        }


        [HttpGet]
        public IActionResult AddToCart(Guid id, bool isCellarstock = false)
        {
            var model = new AddToCartViewModel();
            model.IsCellarstock = isCellarstock;
            model.StockUnits = 1;
            model.StockItemId = id;

            if (isCellarstock)
            {
                model.CellarStockItem = CellarLogic.GetCellarStockItem(id, "Beer");
            }
            else
            {
                model.TappedStockItem = TapLogic.GetTappedStockItem(id, "Beer", "PubLocation");
            }
            return PartialView(model);
        }
        [HttpPost]
        public IActionResult AddToCart(AddToCartViewModel model)
        {

            if (ModelState.IsValid)
            {
                var cartId = GetCartSessionCookie();
                var userId = User.GetUserId();

                Guid CartSessionId;

                CartSessionId = CartSessionLogic.AddStockToCart(cartId, userId, model.StockItemId.Value, model.StockUnits, model.IsCellarstock);



                SetCartSessionCookie(CartSessionId);

                SaveDbChanges();
                model.CloseModal = true;
            }

            if (model.IsCellarstock)
            {
                model.CellarStockItem = CellarLogic.GetCellarStockItem(model.StockItemId.Value, "Beer");
            }
            else
            {
                model.TappedStockItem = TapLogic.GetTappedStockItem(model.StockItemId.Value, "Beer", "PubLocation");
            }

            return PartialView(model);
        }

        [HttpPost]
        public void RemoveFromCart(Guid id)
        {
            CartSessionLogic.RemoveCartItem(id);
            SaveDbChanges();
        }

        

        public IActionResult Cart()
        {
            var model = new CartViewModel();
            model.CartId = GetCartSessionCookie();
            if (model.CartId.HasValue)
            {
                model.Cart = CartSessionLogic.GetCart(model.CartId.Value, "Items", "Items.CellarStockItem", "Items.CellarStockItem.Beer", "Items.TappedStockItem", "Items.TappedStockItem.Beer", "Items.TappedStockItem.PubLocation");
            }
            return PartialView(model);
        }
    }
}
