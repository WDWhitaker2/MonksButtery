using MonksInn.Domain;
using MonksInn.Domain.Interfaces;
using MonksInn.Logic.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MonksInn.Logic
{
    public class PubLocationLogic : BaseLogic
    {
        public PubLocationLogic(IUnitOfWork uow) : base(uow)
        {
        }

        public IQueryable<PubLocation> GetAllPubLocations(params string[] includes)
        {
            return Uow.DbContext.PubLocations.AsQueryable(true, includes);
        }

        public PubLocation Add(PubLocation model)
        {
            model.DateCreated = DateTime.Now;
            return Uow.DbContext.PubLocations.Add(model);
        }

        public bool PubNameExists(string pubName)
        {
            return Uow.DbContext.PubLocations.AsQueryable(true).Any(a => a.Name.Contains(pubName));
        }

        public PubLocation GetLocation(Guid id)
        {
            return Uow.DbContext.PubLocations.AsQueryable(true).FirstOrDefault(a => a.Id == id);
        }

        public void Delete(Guid id)
        {
            var location = Uow.DbContext.PubLocations.AsQueryable(true).FirstOrDefault(a => a.Id == id);
            if (location != null)
            {
                location.IsArchived = true;
            }
        }

        public IQueryable<PubLocation> GetAllPubLocationsForTappedBeer(Guid id)
        {
            var locations = Uow.DbContext.PubLocations.AsQueryable(true)
                .Where(a => a.TappedStockItems.Any(a => a.BeerId == id));
            return locations;
        }
    }
}
