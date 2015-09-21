using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using MongoDB.Driver.Linq;
using Ssi.TrackTruck.Bussiness.DAL.Entities;

namespace Ssi.TrackTruck.Bussiness.DAL
{
    public class MongoRepository: IRepository
    {
        private readonly MongoDatabase _db;
        private readonly Func<Type, string> _mapCollectionName;

        public MongoRepository(MongoDatabase db, Func<Type, string> mapCollectionName)
        {
            _db = db;
            _mapCollectionName = mapCollectionName;
        }

        protected MongoCollection<T> Collection<T>()
        {
            return _db.GetCollection<T>(_mapCollectionName.Invoke(typeof(T)));
        } 

        public T FindOne<T>(Expression<Func<T, bool>> condition)
        {
            return Collection<T>().FindOne(Query<T>.Where(condition));
        }

        public T Create<T>(T item)
        {
            Collection<T>().Insert(item);
            return item;
        }

        public IQueryable<T> GetAll<T>()
        {
            return Collection<T>().AsQueryable();
        }

        public IQueryable<T> WhereIn<T, TProp>(Expression<Func<T, TProp>> property, IEnumerable<TProp> values)
        {
            return Collection<T>().Find(Query<T>.In(property, values)).AsQueryable();
        }

        public IQueryable<T> GetAllProjected<T>(params Expression<Func<T, object>>[] property)
        {
            return Collection<T>().FindAll().SetFields(Fields<T>.Include(property)).AsQueryable();
        }

        public bool Exists<T>(Expression<Func<T, bool>> condition)
        {
            return Collection<T>().Find(Query<T>.Where(condition)).SetFields("_id").Any();
        }

        public void CreateAll<T>(IEnumerable<T> items)
        {
            Collection<T>().InsertBatch(items);
        }

        public T SoftDelete<T>(string id) where T : IEntity, ISoftDeletable
        {
            var item = FindOne<T>(e => e.Id == id && !e.IsDeleted);
            if (item != null)
            {
                item.IsDeleted = true;
                Collection<T>().Save(item);
            }

            return item;
        }

        public IQueryable<T> GetAllUndeleted<T>() where T : ISoftDeletable
        {
            return Collection<T>().Find(Query<T>.EQ(e => e.IsDeleted, false)).AsQueryable();
        }
    }
}
