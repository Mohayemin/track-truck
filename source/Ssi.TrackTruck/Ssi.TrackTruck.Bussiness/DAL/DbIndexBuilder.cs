using System;
using System.Linq.Expressions;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using Ssi.TrackTruck.Bussiness.DAL.Entities;
using Ssi.TrackTruck.Bussiness.DAL.Trips;
using Ssi.TrackTruck.Bussiness.DAL.Users;

namespace Ssi.TrackTruck.Bussiness.DAL
{
    public class DbIndexBuilder
    {
        private readonly MongoDatabase _db;
        private readonly CollectionMapper _mapper;

        public DbIndexBuilder(MongoDatabase db, CollectionMapper mapper)
        {
            _db = db;
            _mapper = mapper;
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
                trip => trip.HelperIds,
                trip => trip.Status);
        }

        private void BuildIndex<T>(params Expression<Func<T, object>>[] indexes)
        {
            foreach (var index in indexes)
            {
                var collectionName = _mapper.Map(typeof (T));
                _db.GetCollection<T>(collectionName).CreateIndex(IndexKeys<T>.Ascending(index));
            }
        }
    }
}
