using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Ssi.TrackTruck.Bussiness.DAL
{
    public interface IRepository
    {
        T FindOne<T>(Expression<Func<T, bool>> condition);
        T Create<T>(T item);
        IQueryable<T> GetAll<T>();
        IQueryable<T> WhereIn<T, TProp>(Expression<Func<T, TProp>> property, IEnumerable<TProp> values);
        IQueryable<T> GetAllProjected<T>(params Expression<Func<T, object>>[] property);
    }
}
