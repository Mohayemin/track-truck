using System.Collections.Generic;
using System.Linq;
using Ssi.TrackTruck.Bussiness.Auth;
using Ssi.TrackTruck.Bussiness.DAL;
using Ssi.TrackTruck.Bussiness.DAL.Clients;
using Ssi.TrackTruck.Bussiness.DAL.Constants;
using Ssi.TrackTruck.Bussiness.DAL.Trips;

namespace Ssi.TrackTruck.Bussiness.Trips
{
    public class TripService
    {
        private readonly IRepository _repository;
        private readonly ISignedInUser _user;

        public TripService(IRepository repository, ISignedInUser user)
        {
            _repository = repository;
            _user = user;
        }

        public DbTrip AddTrip(TripOrderRequest orderRequest)
        {
            var trip = orderRequest.ToTrip();
            return _repository.Create(trip);
        }

        public IEnumerable<DbTrip> GetAll()
        {
            return _repository.GetAll<DbTrip>();
        }

        public IQueryable<DbTrip> GetActiveTrips()
        {
            var activeTrips = _repository.GetWhere<DbTrip>(
                trip => trip.Status == TripStatus.NotStarted || trip.Status == TripStatus.OnTheWay);
            return activeTrips;
        }

        public IEnumerable<string> GetMyActiveTrips()
        {
            var myBrancheIds =
                _repository.GetWhere<DbClient>(
                    client => client.Branches.Any(branch => branch.CustodianUserId == _user.Id))
                    .SelectMany(client => client.Branches)
                    .Select(branch => branch.Id);

            return GetActiveTrips()
                .Where(trip => trip.Drops.Any(drop => myBrancheIds.Contains(drop.BranchId)))
                .Select(trip => trip.Id);
        }
    }
}
