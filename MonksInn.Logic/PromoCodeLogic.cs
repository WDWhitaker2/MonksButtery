using MonksInn.Domain;
using MonksInn.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MonksInn.Logic
{
    public class PromoCodeLogic : BaseLogic
    {
        public PromoCodeLogic(IUnitOfWork uow) : base(uow)
        {
        }



        public bool ValidatePromoCode(string code)
        {
            var promocode = Uow.DbContext.PromoCodes.AsQueryable(true).FirstOrDefault(a => a.Code == code);
            return promocode != null;
        }

        internal void UsePromoCode(PromoCode promo)
        {
            promo.SpentCount++;
            //throw new NotImplementedException();
        }

        public IQueryable<PromoCode> GetAllCodes()
        {
            return Uow.DbContext.PromoCodes.AsQueryable(true);
        }

        public PromoCode Add(PromoCode promoCode)
        {
            promoCode.DateCreated = DateTime.Now;
            return Uow.DbContext.PromoCodes.Add(promoCode);
        }

        public PromoCode GetCode(Guid id)
        {
            return Uow.DbContext.PromoCodes.AsQueryable(true).FirstOrDefault(a=>a.Id == id);
        }

        public void Delete(Guid id)
        {
            var codes = Uow.DbContext.PromoCodes.AsQueryable(true).FirstOrDefault(a => a.Id == id);
            if (codes != null)
            {
                codes.IsArchived = true;
            }
        }

        internal decimal GetDiscountedAmount(Order order, PromoCode promo)
        {
            //todo logic here will determine the amount discounted
            return 0m;
        }
    }
}
