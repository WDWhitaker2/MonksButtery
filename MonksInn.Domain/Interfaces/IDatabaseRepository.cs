using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace MonksInn.Domain.Interfaces
{
    public interface IDatabaseRepository<T> where T : DatabaseObject
    {
        public IQueryable<T> AsQueryable(bool ExcludeArchived = true, params string[] includes);
        public T Add(T model);
        void Remove(T cartitem);
        void Remove(List<T> cartitem);
    }
}
