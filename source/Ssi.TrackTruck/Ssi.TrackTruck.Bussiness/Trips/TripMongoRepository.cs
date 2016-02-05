using System;
using System.Collections.Generic;
using System.Linq;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using MongoDB.Driver.Linq;
using Ssi.TrackTruck.Bussiness.DAL;
using Ssi.TrackTruck.Bussiness.DAL.Clients;
using Ssi.TrackTruck.Bussiness.DAL.Constants;
using Ssi.TrackTruck.Bussiness.DAL.Trips;

namespace Ssi.TrackTruck.Bussiness.Trips
{

    public class TripMongoRepository : ITripRepository
    {
        private readonly MongoCollection<DbTrip> _trips;
        private readonly MongoCollection<DbTripDrop> _drops;
        private readonly MongoCollection<DbClient> _clients;

        public static TripStatus[] ActiveTripStatuses = { TripStatus.New, TripStatus.InProgress };

        public TripMongoRepository(MongoRepository repository)
        {
            MongoRepository repository1 = repository;
            _trips = repository1.Collection<DbTrip>();
            _drops = repository1.Collection<DbTripDrop>();
            _clients = repository1.Collection<DbClient>();
        }

        public IQueryable<DbTrip> GetTripsInRange(DateTime fromUtc, DateTime toUtc)
        {
            var trips =
                _trips.AsQueryable().Where(trip => trip.ExpectedPickupTimeUtc >= fromUtc && trip.ExpectedPickupTimeUtc <= toUtc);

            return trips;
        }

        public IQueryable<DbTripDrop> GetDropsOfTrips(IEnumerable<string> tripIds)
        {
            return _drops.Find(Query<DbTripDrop>.In(drop => drop.TripId, tripIds)).AsQueryable();
        } 

        public DbTripDrop GetDrop(string dropId)
        {
            var drop = _drops.FindOneById(ObjectId.Parse(dropId));
            if (drop != null)
            {
                drop = BsonSerializer.Deserialize<DbTripDrop>(drop.ToBson());
            }
            return drop;
        }
    }
}
