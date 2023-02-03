using MonksInn.Domain;
using MonksInn.Domain.Enums;
using MonksInn.Domain.Interfaces;
using MonksInn.Logic.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MonksInn.Logic
{
    public class PricingStructureLogic : BaseLogic
    {
        public PricingStructureLogic(IUnitOfWork uow) : base(uow)
        {
        }

        public IQueryable<StoreDefaultPricing> GetAllDefaultPrices(params string[] includes)
        {
            return Uow.DbContext.StoreDefaultPricings.AsQueryable(true, includes);
        }
        public bool CheckWholesaleUnitSizeIsAvailable(string unitSize, Guid? priceId = null)
        {

            var wholesaledefaultPricing = Uow.DbContext.StoreDefaultPricings.AsQueryable(true);
            if (priceId.HasValue)
            {
                wholesaledefaultPricing = wholesaledefaultPricing.Where(a => a.Id != priceId);
            }

            return !wholesaledefaultPricing.Any(a => a.WholeSaleUnitSize.ToLower() == unitSize);
        }

        public StoreDefaultPricing Add(StoreDefaultPricing storeDefaultPricing)
        {
            storeDefaultPricing.DateCreated = DateTime.Now;
            return Uow.DbContext.StoreDefaultPricings.Add(storeDefaultPricing);
        }

        public StoreDefaultPricing GetStoreDefaultPrice(Guid id)
        {
            return Uow.DbContext.StoreDefaultPricings.AsQueryable(true).FirstOrDefault(a => a.Id == id);
        }

        public bool CheckRetailOptionIsAvailable(BeerType? retailBeerType, decimal? retailAbvLimit, Guid? priceId = null)
        {
            var wholesaledefaultPricing = Uow.DbContext.StoreDefaultPricings.AsQueryable(true);
            if (priceId.HasValue)
            {
                wholesaledefaultPricing = wholesaledefaultPricing.Where(a => a.Id != priceId);
            }

            return !wholesaledefaultPricing.Any(a => a.RetailBeerType == retailBeerType && a.RetailAbvLimit == retailAbvLimit);
        }

        public void Delete(Guid id)
        {
            var order = Uow.DbContext.StoreDefaultPricings.AsQueryable(true).FirstOrDefault(a => a.Id == id);
            if (order != null)
            {
                order.IsArchived = true;
            }
        }
    }
}
