using MonksInn.Domain;
using MonksInn.Domain.Enums;
using MonksInn.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonksInn.Logic
{
    public class WholesaleApplicationLogic : BaseLogic
    {
        public WholesaleApplicationLogic(IUnitOfWork uow) : base(uow)
        {
        }

        public IQueryable<WholesaleApplication> GetAllApplications(params string[] includes)
        {
            return Uow.DbContext.WholesaleApplications.AsQueryable(true, includes);
        }

        public WholesaleApplicationResult? GetWholesaleApplicationResult(Guid id)
        {
            var application = Uow.DbContext.WholesaleApplications.AsQueryable(true).FirstOrDefault(a => a.StoreUserId == id);
            return application?.Result;
        }

        public WholesaleApplication GetWholesaleApplication(Guid id, params string[] includes)
        {
            return Uow.DbContext.WholesaleApplications.AsQueryable(true, includes).FirstOrDefault(a => a.Id == id);
        }

        public void UpdateApplication(Guid id, bool result)
        {
            var application = GetWholesaleApplication(id, "StoreUser");
            
            application.StoreUser.IsWholeSaleUser = result;
            application.Result = result ? Domain.Enums.WholesaleApplicationResult.Accepted : Domain.Enums.WholesaleApplicationResult.Rejected;

            Uow.EmailService.SendWholesaleApplicationResponseEmail(application, application.StoreUser);

        }
    }
}
