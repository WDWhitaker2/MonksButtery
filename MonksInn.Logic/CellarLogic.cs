using MonksInn.Domain;
using MonksInn.Domain.Interfaces;
using MonksInn.Logic.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MonksInn.Logic
{
    public class CellarLogic : BaseLogic
    {
        public CellarLogic(IUnitOfWork uow) : base(uow)
        {
        }

        public IQueryable<CellarStockItem> GetAllStockItems(params string[] includes)
        {
            return Uow.DbContext.CellarStockItems.AsQueryable(true, includes);
        }

        public CellarStockItem GetCellarStockItem(Guid id, params string[] includes)
        {
            return Uow.DbContext.CellarStockItems.AsQueryable(true, includes).FirstOrDefault(a => a.Id == id);
        }
        public List<string> TapTypes()
        {
            return new List<string>()
            {
                "Conditioning",
                "Gravity",
                "Handle",
                "Fizz",
            };
        }

        public TappedStockItem TapBeer(Guid cellarStockItemId, string tapType, Guid pubLocationId)
        {

            //archive cellar stock
            var cellarStock = Uow.DbContext.CellarStockItems.AsQueryable(false).Where(a => a.Id == cellarStockItemId).FirstOrDefault();
            cellarStock.IsArchived = true;
            cellarStock.ArchiveReason = $"This beer was tapped on the {DateTime.Now.ToString("dd MMM yyyy")}.";

            var location = Uow.DbContext.PubLocations.AsQueryable(false).FirstOrDefault(a => a.Id == pubLocationId);

            //add tapped stock
            var tappedStock = new TappedStockItem()
            {
                BeerId = cellarStock.BeerId,
                TapType = tapType,
                DateCreated = DateTime.Now,
                UnitSize = cellarStock.UnitSize,
                PubLocationId = pubLocationId,
                ForDelivery = location?.DefaultIsDeliveryLocation ?? false,
                ForTakeaway = location?.DefaultIsTakeawayLocation ?? false,
                RetailPrice = GetDefaultRetailPrice(cellarStock.BeerId)
            };

            return Uow.DbContext.TappedStockItems.Add(tappedStock);
        }



        private decimal? GetDefaultRetailPrice(Guid beerId)
        {
            var beer = Uow.DbContext.Beers.AsQueryable(false).FirstOrDefault(a => a.Id == beerId);

            decimal? defaultprice = null;
            if (beer.IsSpecialityBeer)
            {
                // get the heighest price for special beers  for the associated beer type
                defaultprice = Uow.DbContext.StoreDefaultPricings
                    .AsQueryable(true)
                    .Where(a => !a.IsWholesalePricing)
                    .ToList()
                    .Where(a => a.RetailBeerType == beer.BeerType || !a.RetailBeerType.HasValue)
                    .OrderBy(a => !a.RetailBeerType.HasValue)
                    .ThenByDescending(a => a.RetailAbvLimit)
                    .FirstOrDefault()?.RetailDefaultAbvPrice;
            }
            else
            {

                // get the price for  the correct ABV bracket for the associated beer type 
                defaultprice = Uow.DbContext.StoreDefaultPricings
                    .AsQueryable(true)
                    .Where(a => !a.IsWholesalePricing)
                    .ToList()
                    .Where(a => a.RetailBeerType == beer.BeerType || !a.RetailBeerType.HasValue)
                    .Where(a => a.RetailAbvLimit >= beer.Strength)
                    .OrderBy(a => !a.RetailBeerType.HasValue)
                    .ThenBy(a => a.RetailAbvLimit)
                    .FirstOrDefault()?.RetailDefaultAbvPrice;
            }

            return defaultprice;

        }

        public void AddStockItem(CellarStockItem stockitem)
        {
            stockitem.DateCreated = DateTime.Now;
            Uow.DbContext.CellarStockItems.Add(stockitem);
        }

        public void UpdateCellarStockOrderValidation(CellarStockItem stockitem)
        {
            //throw new NotImplementedException();
        }

        public void Delete(Guid id, string reason)
        {
            var order = Uow.DbContext.CellarStockItems.AsQueryable(true).FirstOrDefault(a => a.Id == id);
            if (order != null)
            {
                order.IsArchived = true;
                order.ArchiveReason = reason;
            }
        }

        /// <summary>
        /// gets the first available cellar stock item based on the beer id. 
        /// excludes items that have been added to a cart in the last 1 hour. 
        /// excudes items in the users cart,
        /// </summary>
        /// <param name="beerId"></param>
        /// <param name="includes"></param>
        /// <returns></returns>
        public CellarStockItem GetAvailableStockItem(Guid beerId, Guid? usercartsession,  params string[] includes)
        {
            var cartperiod = DateTime.Now.AddHours(1);
            
            var cartitems = Uow.DbContext.CartItems.AsQueryable(true)
                .Where(a => !(a.DateCreated >= cartperiod))
                .Select(a => a.CellarStockItemId)
                .Distinct()
                .ToList();

            var orderitems = Uow.DbContext.OrderItems.AsQueryable(true)
                .Where(a=>a.Order.OrderStatus != Domain.Enums.OrderStatus.Cancelled || a.Order.OrderStatus != Domain.Enums.OrderStatus.Returned)
                .Select(a=>a.CellarStockItemId)
                .Distinct()
                .ToList();

            var usercartitems = Uow.DbContext.CartItems.AsQueryable(true)
                .Where(a=> usercartsession.HasValue && a.CartSessionId == usercartsession)
                .Select(a=>a.CellarStockItemId)
                .Distinct()
                .ToList();

            var stockitems = Uow.DbContext.CellarStockItems.AsQueryable(true, includes)
                .Where(a => a.BeerId == beerId)
                .Where(a=> !cartitems.Contains(a.Id))
                .Where(a=> !orderitems.Contains(a.Id))
                .Where(a=> !usercartitems.Contains(a.Id))
                .ToList();

            return stockitems.FirstOrDefault();

        }

        public List<CellarStockItem> GetAvailableStockItems(Guid? usercartsession, params string[] includes)
        {
            var cartperiod = DateTime.Now.AddHours(1);

            var cartitems = Uow.DbContext.CartItems.AsQueryable(true)
                .Where(a => !(a.DateCreated >= cartperiod))
                .Select(a => a.CellarStockItemId)
                .Distinct()
                .ToList();

            var orderitems = Uow.DbContext.OrderItems.AsQueryable(true)
                .Where(a => a.Order.OrderStatus != Domain.Enums.OrderStatus.Cancelled || a.Order.OrderStatus != Domain.Enums.OrderStatus.Returned)
                .Select(a => a.CellarStockItemId)
                .Distinct()
                .ToList();


            var usercartitems = Uow.DbContext.CartItems.AsQueryable(true)
                .Where(a => !usercartsession.HasValue || a.CartSessionId == usercartsession)
                .Select(a => a.CellarStockItemId)
                .Distinct()
                .ToList();

            var stockitems = Uow.DbContext.CellarStockItems.AsQueryable(true, includes)
                .Where(a => !cartitems.Contains(a.Id))
                .Where(a => !orderitems.Contains(a.Id))
                .Where(a => !usercartitems.Contains(a.Id))
                .ToList();

            return stockitems;

        }

        public List<string> GetAvailableUnitSizesForBeer(Guid beerId, Guid? usercartsession, params string[] includes)
        {
            var availablestockitems = GetAvailableStockItems(usercartsession, includes);

            return availablestockitems.Where(a => a.BeerId == beerId).Select(a => a.UnitSize).Distinct().ToList();
        }
    }
}
