using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Ssi.TrackTruck.Bussiness.DAL.Entities;

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

        public T Create<T>(T item) where T:IEntity
        {
            item.CreationTime = DateTime.UtcNow;
            List<T>().Add(item);
            return item;
        }

        public void CreateAll<T>(IEnumerable<T> items) where T : IEntity
        {
            List<T>().AddRange(items);
        }

        public IQueryable<T> GetAll<T>()
        {
            return Query<T>();
        }

        public T GetById<T>(string id) where T : IEntity
        {
            return Query<T>().FirstOrDefault(arg => arg.Id == id);
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

        public bool Exists<T>(Expression<Func<T, bool>> condition) where T : IEntity
        {
            return Query<T>().Any(condition);
        }

        public T SoftDelete<T>(string id) where T : IEntity
        {
            var item = FindOne<T>(e => e.Id == id);
            if (item != null)
            {
                item.IsDeleted = true;
            }
            return item;
        }

        public IQueryable<T> GetAllUndeleted<T>() where T : IEntity
        {
            return GetAll<T>().Where(e => !e.IsDeleted);
        }

        public T Save<T>(T item)
        {
            return item;
        }

        public IQueryable<T> GetWhere<T>(Expression<Func<T, bool>> condition)
        {
            return GetAll<T>().Where(condition);
        }

        private IQueryable<T> Query<T>()
        {
            return List<T>().AsQueryable();
        }

        private List<T> List<T>()
        {
            var type = typeof(T);
            if (!_data.ContainsKey(type))
            {
                _data[type] = new List<T>();
            }

            return (List<T>)_data[type];
        }
    }
}
