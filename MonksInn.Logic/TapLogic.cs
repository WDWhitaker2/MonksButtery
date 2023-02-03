using MonksInn.Domain;
using MonksInn.Domain.Interfaces;
using MonksInn.Logic.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MonksInn.Logic
{
    public class TapLogic : BaseLogic
    {
        public TapLogic(IUnitOfWork uow) : base(uow)
        {
        }

        public IQueryable<TappedStockItem> GetAllStockItems(params string[] includes)
        {
            return Uow.DbContext.TappedStockItems.AsQueryable(true, includes);
        }


        public TappedStockItem GetTappedStockItem(Guid id, params string[] includes)
        {
            return Uow.DbContext.TappedStockItems.AsQueryable(true, includes).FirstOrDefault(a => a.Id == id);
        }

        public void Delete(Guid id)
        {
            var beer = Uow.DbContext.TappedStockItems.AsQueryable(true).FirstOrDefault(a => a.Id == id);
            if (beer != null)
            {
                beer.IsArchived = true;
            }
        }
    }
}
