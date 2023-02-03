using MonksInn.Domain;
using MonksInn.Domain.Enums;
using MonksInn.Domain.Interfaces;
using MonksInn.Logic.Extensions;
using MonksInn.Logic.Models.StockOrderLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MonksInn.Logic
{
    public class StockOrderLogic : BaseLogic
    {
        public StockOrderLogic(IUnitOfWork uow) : base(uow)
        {

        }


        public List<string> GetUnitSizes()
        {
            return new List<string>()
            {
                "20L Box",
                "30L Keg",
                "50L Keg",
                "Firkin",
                "Pin",
                "Polly",
            };

        }

        public StockOrder Add(AddLogicModel model)
        {

            var stockOrderModel = new StockOrder()
            {
                BeerId = model.BeerId,
                UnitSize = model.UnitSize,
                Units = model.Units,
                ETA = model.ETA,
                CreateWeeklyOrder = model.CreateWeeklyOrder,
                DateCreated = DateTime.Now

            };

            return Uow.DbContext.StockOrders.Add(stockOrderModel);


        }

        public IQueryable<StockOrder> GetAllStockOrders(params string[] includes)
        {
            return Uow.DbContext.StockOrders.AsQueryable(true, includes);
        }

        public void Delete(Guid id)
        {
            var order = Uow.DbContext.StockOrders.AsQueryable(true).FirstOrDefault(a => a.Id == id);
            if (order != null)
            {
                order.IsArchived = true;
            }
        }

        public StockOrder GetStockOrder(Guid id, params string[] includes)
        {
            return Uow.DbContext.StockOrders.AsQueryable(true, includes).FirstOrDefault(a => a.Id == id);
        }


        public void ReceiveStock(ReceiveStockLogicModel model)
        {
            //update stock order
            var stockorder = Uow.DbContext.StockOrders.AsQueryable(true).FirstOrDefault(a => a.Id == model.Id);
            stockorder.ReceivedDate = DateTime.Now;
            stockorder.ReceivedInvoiceNumber = model.InvoiceNumber;
            stockorder.ReceivedUnitPrice = model.UnitPrice;
            stockorder.ReceivedUnits = model.Units;
            stockorder.ReceivedUnitSize = model.UnitSize;
            stockorder.ReceivedBeerId = model.BeerId;


            var beer = Uow.DbContext.Beers.AsQueryable(false).FirstOrDefault(a => a.Id == model.BeerId);
            beer.Strength = model.BeerStrength;


            // archive an acceptable order
            if (!stockorder.ValidationErrors().Any())
            {
                stockorder.IsArchived = true;
            }

            // create cellar stock
            for (int i = 0; i < model.Units; i++)
            {
                var newstock = new CellarStockItem()
                {
                    BeerId = model.BeerId,
                    CostPrice = model.UnitPrice,
                    DateCreated = DateTime.Now,
                    UnitSize = model.UnitSize,
                    SellByDate = model.SellByDate,
                    WholesalePrice = GetDefaultWholesalePriceForStock(stockorder),
                };
                Uow.DbContext.CellarStockItems.Add(newstock);
            }


        }

        public decimal GetDefaultWholesalePriceForPrice(string unitSize, decimal price)
        {
            decimal? defaultmarkup = GetWholesalePriceForUnitSize(unitSize);

            return (decimal)(price + defaultmarkup);

        }

        private decimal? GetWholesalePriceForUnitSize(string unitSize)
        {
            return Uow.DbContext.StoreDefaultPricings
                  .AsQueryable(true)
                  .Where(a => a.IsWholesalePricing)
                  .ToList()
                  .Where(a => a.WholeSaleUnitSize == unitSize || string.IsNullOrWhiteSpace(a.WholeSaleUnitSize))
                  .OrderBy(a => string.IsNullOrWhiteSpace(a.WholeSaleUnitSize))
                  .ThenByDescending(a => a.IsWholesalePricing)
                  .FirstOrDefault()?.WholeSaleDefaultFlatMarkup;
        }

        public decimal? GetDefaultWholesalePriceForStock(StockOrder stockorder)
        {
            var unitcost = stockorder.ReceivedUnitPrice;
            if (unitcost.HasValue)
            {

                decimal? defaultmarkup = GetWholesalePriceForUnitSize(stockorder.ReceivedUnitSize);

                return unitcost + defaultmarkup;

            }
            return null;
        }

    }
}
