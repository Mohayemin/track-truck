using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using MongoDB.Driver.Linq;
using Ssi.TrackTruck.Bussiness.DAL.Entities;
using Ssi.TrackTruck.Bussiness.DAL.Trips;
using Ssi.TrackTruck.Bussiness.DAL.Users;

namespace Ssi.TrackTruck.Bussiness.DAL
{
    public class MongoRepository : IRepository
    {
        private readonly MongoDatabase _db;
        private readonly Func<Type, string> _mapCollectionName;

        public MongoRepository(MongoDatabase db, Func<Type, string> mapCollectionName)
        {
            _db = db;
            _mapCollectionName = mapCollectionName;
        }

        public void BuildIndexes()
        {
            BuildIndex<DbUser>(user => user.UsernameLowerCase);
            
            BuildIndex<DbDailyHit>(
                hit => hit.Date, 
                hit => hit.UserId);

            BuildIndex<DbTrip>(
                trip => trip.ClientId,
                trip => trip.DriverId, 
                trip => trip.HelperId, 
                trip => trip.Status);
        }

        private void BuildIndex<T>(params Expression<Func<T, object>>[] indexes)
        {
            foreach (var index in indexes)
            {
                Collection<T>().CreateIndex(IndexKeys<T>.Ascending(index));
            }
        }


        protected MongoCollection<T> Collection<T>()
        {
            return _db.GetCollection<T>(_mapCollectionName.Invoke(typeof(T)));
        }

        public T FindOne<T>(Expression<Func<T, bool>> condition)
        {
            return Collection<T>().FindOne(Query<T>.Where(condition));
        }

        public T Create<T>(T item) where T:IEntity
        {
            item.IsDeleted = false;
            item.CreationTime = DateTime.UtcNow;
            Collection<T>().Insert(item);
            return item;
        }

        public IQueryable<T> GetAll<T>()
        {
            return Collection<T>().AsQueryable();
        }

        public T GetById<T>(string id) where T : IEntity
        {
            return Collection<T>().FindOneById(ObjectId.Parse(id));
        }

        public IQueryable<T> WhereIn<T, TProp>(Expression<Func<T, TProp>> property, IEnumerable<TProp> values)
        {
            return Collection<T>().Find(Query<T>.In(property, values)).AsQueryable();
        }

        public IQueryable<T> GetAllProjected<T>(params Expression<Func<T, object>>[] property)
        {
            return Collection<T>().FindAll().SetFields(Fields<T>.Include(property)).AsQueryable();
        }

        public bool Exists<T>(Expression<Func<T, bool>> condition) where T : IEntity
        {
            return Collection<T>().Find(Query.And(Query<T>.Where(condition), Query<T>.EQ(e => e.IsDeleted, false))).SetFields("_id").Any();
        }

        public T SoftDelete<T>(string id) where T : IEntity
        {
            var item = FindOne<T>(e => e.Id == id && !e.IsDeleted);
            if (item != null)
            {
                item.IsDeleted = true;
                Collection<T>().Save(item);
            }

            return item;
        }

        public IQueryable<T> GetAllUndeleted<T>() where T : IEntity
        {
            return Collection<T>().Find(Query<T>.EQ(e => e.IsDeleted, false)).AsQueryable();
        }

        public T Save<T>(T item)
        {
            Collection<T>().Save(item);
            return item;
        }

        public IQueryable<T> GetWhere<T>(Expression<Func<T, bool>> condition)
        {
            return Collection<T>().Find(Query<T>.Where(condition)).AsQueryable();
        }
    }
}
