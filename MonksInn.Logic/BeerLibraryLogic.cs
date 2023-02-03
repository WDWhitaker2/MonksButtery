using MonksInn.Domain;
using MonksInn.Domain.Interfaces;
using MonksInn.Logic.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MonksInn.Logic
{
    public class BeerLibraryLogic : BaseLogic
    {
        public BeerLibraryLogic(IUnitOfWork uow) : base(uow)
        {
        }


        public Beer Add(Beer model)
        {
            model.DateCreated = DateTime.Now;
            
            //if (!string.IsNullOrWhiteSpace(model.Type))
            //    model.Type = model.Type.ToTitleCase();

            return Uow.DbContext.Beers.Add(model);
        }

        public IQueryable<Beer> GetAllBeers(params string[] includes)
        {
            return Uow.DbContext.Beers.AsQueryable(true, includes);
        }
        public IQueryable<Beer> GetAllBeers(Guid? includedArchivedBeerId, params string[] includes)
        {
            return Uow.DbContext.Beers.AsQueryable(false, includes).Where(a => !a.IsArchived || (a.IsArchived && a.Id == includedArchivedBeerId));
        }

        public List<string> GetAllBreweryNames()
        {
            return Uow.DbContext.Beers.AsQueryable(true).Select(a => a.BreweryName).Distinct().ToList().Where(a => !string.IsNullOrWhiteSpace(a)).ToList();
        }

        public List<string> GetAllBeerSubTypes()
        {
            return Uow.DbContext.Beers.AsQueryable(true).Select(a => a.SubType).Distinct().ToList().Where(a => !string.IsNullOrWhiteSpace(a)).ToList();
        }

        public Beer GetBeer(Guid id)
        {
            return Uow.DbContext.Beers.AsQueryable(true).FirstOrDefault(a => a.Id == id);
        }

        public void Delete(Guid id)
        {
            var beer = Uow.DbContext.Beers.AsQueryable(true).FirstOrDefault(a => a.Id == id);
            if(beer != null)
            {
                beer.IsArchived = true;
            }
        }

       
    }
}
