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

        public static TripStatus[] ActiveTripStatuses = { TripStatus.NotStarted, TripStatus.OnTheWay };

        public TripMongoRepository(MongoRepository repository)
        {
            MongoRepository repository1 = repository;
            _trips = repository1.Collection<DbTrip>();
            _drops = repository1.Collection<DbTripDrop>();
            _clients = repository1.Collection<DbClient>();
        }

        public IQueryable<DbTripDrop> GetUsersActiveDrops(string userId)
        {
            var activeTripIds =
                _trips.Find(Query<DbTrip>.In(trip => trip.Status, ActiveTripStatuses))
                    .SetFields(Fields<DbTrip>.Include(trip => trip.Id))
                    .Select(trip => trip.Id);

            var userBrancheIds = GetUserBranchIds(userId);

            return _drops.Find(Query.And(Query<DbTripDrop>.EQ(drop => drop.IsReceived, false), Query<DbTripDrop>.In(drop => drop.BranchId, userBrancheIds),
                Query<DbTripDrop>.In(drop => drop.TripId, activeTripIds))).AsQueryable();
        }

        public IEnumerable<string> GetUserBranchIds(string userId)
        {
            var branchQuery = Query<DbBranch>.EQ(branch => branch.CustodianUserId, userId);

            var userBrancheIds =
                _clients.Find(Query<DbClient>.ElemMatch(client => client.Branches, builder => branchQuery))
                    .SelectMany(client => client.Branches)
                    .Where(branch => branch.CustodianUserId == userId)
                    .Select(branch => branch.Id);
            return userBrancheIds;
        }

        public IQueryable<DbTrip> GetTripsInRange(DateTime from, DateTime to)
        {
            var trips =
                _trips.AsQueryable().Where(trip => trip.ExpectedPickupTime >= from && trip.ExpectedPickupTime <= to);

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
