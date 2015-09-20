using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Ssi.TrackTruck.Bussiness.DAL
{
    public class ListStorageRepository : IRepository
    {
        private IDictionary<Type, IList> _data;
        public ListStorageRepository(IDictionary<Type, IList> data)
        {
            _data = data;
        }

        public T FindOne<T>(Expression<Func<T, bool>> condition)
        {
            return Query<T>().FirstOrDefault(condition);
        }

        public T Create<T>(T item)
        {
            List<T>().Add(item);
            return item;
        }

        public IQueryable<T> GetAll<T>()
        {
            return Query<T>();
        }

        public IQueryable<T> WhereIn<T, TProp>(Expression<Func<T, TProp>> property, IEnumerable<TProp> values)
        {
            var valueList = values.ToList();
            return Query<T>().Where(t => valueList.Contains(property.Compile().Invoke(t)));
        }

        public IQueryable<T> GetAllProjected<T>(params Expression<Func<T, object>>[] property)
        {
            return GetAll<T>();
        }

        public bool Exists<T>(Expression<Func<T, bool>> condition)
        {
            return Query<T>().Any(condition);
        }

        private IQueryable<T> Query<T>()
        {
            return List<T>().AsQueryable();
        }

        private IList<T> List<T>()
        {
            var type = typeof(T);
            if (!_data.ContainsKey(type))
            {
                _data[type] = new List<T>();
            }

            return (IList<T>) _data[type];
        }
    }
}
