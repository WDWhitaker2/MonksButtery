using Microsoft.EntityFrameworkCore;
using MonksInn.Domain;
using MonksInn.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace MonksInn.SqlDataService
{
    public class SqlDataRepository<T> : IDatabaseRepository<T> where T : DatabaseObject
    {
        DbSet<T> dbset { get; set; }

        public SqlDataRepository(DbSet<T> _dbset)
        {
            dbset = _dbset;
        }

        public T Add(T model)
        {

            return dbset.Add(model).Entity;
        }
     
        public void Remove(T cartitem)
        {
            dbset.Remove(cartitem);
        }

        public void Remove(List<T> cartitem)
        {
            dbset.RemoveRange(cartitem);
        }

        public IQueryable<T> AsQueryable(bool ExcludeArchived, params string[] includes)
        {
            var query = dbset.AsQueryable();
            if (includes.Any())
            {
                foreach (var item in includes)
                {
                    query = query.Include(item);
                }
            }

            if (ExcludeArchived)
            {
                return query.Where(a => !a.IsArchived);
            }
            return query.AsQueryable();

        }

    }
}
