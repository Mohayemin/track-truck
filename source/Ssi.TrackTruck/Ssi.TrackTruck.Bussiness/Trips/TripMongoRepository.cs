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
        private readonly MongoRepository _repository;
        private readonly MongoCollection<DbTrip> _trips;
        private readonly MongoCollection<DbTripDrop> _drops;
        private readonly MongoCollection<DbTripContract> _contracts;

        public static TripStatus[] ActiveTripStatuses = { TripStatus.New, TripStatus.InProgress };

        public TripMongoRepository(MongoRepository repository)
        {
            _repository = repository;
            _trips = repository.Collection<DbTrip>();
            _drops = repository.Collection<DbTripDrop>();
            _contracts = repository.Collection<DbTripContract>();
        }

        public IQueryable<DbTrip> GetTripsInRange(DateTime fromUtc, DateTime toUtc)
        {
            var trips =
                _trips.AsQueryable().Where(trip => !trip.IsDeleted && trip.ExpectedPickupTimeUtc >= fromUtc && trip.ExpectedPickupTimeUtc <= toUtc);

            return trips;
        }

        public IDictionary<string, IEnumerable<DbTripDrop>> GetIndexedDrops(IEnumerable<string> tripIds)
        {
            return _repository.WhereIn<DbTripDrop, string>(contract => contract.TripId, tripIds)
                .GroupBy(contract => contract.TripId)
                .ToDictionary(g => g.Key, g => g.AsEnumerable());
        }

        public IDictionary<string, IEnumerable<DbTripContract>> GetIndexedContracts(IEnumerable<string> tripIds)
        {
            return _repository.WhereIn<DbTripContract, string>(contract => contract.TripId, tripIds)
                .GroupBy(contract => contract.TripId)
                .ToDictionary(g => g.Key, g => g.AsEnumerable());
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
