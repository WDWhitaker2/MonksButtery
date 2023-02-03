using MonksInn.Domain;
using MonksInn.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MonksInn.Logic
{
    public class SystemSettingsLogic : BaseLogic
    {
        public SystemSettingsLogic(IUnitOfWork uow) : base(uow)
        {
        }


        public SystemSettings GetSystemSettings()
        {
            return Uow.DbContext.SystemSettings.AsQueryable(false).FirstOrDefault();
        }

        public SystemSettings GenerateSettings()
        {
            return Uow.DbContext.SystemSettings.Add(new SystemSettings
            {
                DateCreated = DateTime.Now
            });
        }
    }
}
