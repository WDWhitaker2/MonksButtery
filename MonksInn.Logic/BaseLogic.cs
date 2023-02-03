using MonksInn.Domain.Interfaces;
using System;

namespace MonksInn.Logic
{
    public class BaseLogic
    {
        internal IUnitOfWork Uow { get; set; }
        public BaseLogic(IUnitOfWork uow)
        {
            Uow = uow;
        }
    }
}
