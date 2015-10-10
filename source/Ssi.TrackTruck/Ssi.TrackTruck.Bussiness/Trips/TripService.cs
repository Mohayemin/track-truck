using System.Collections.Generic;
using System.Linq;
using Ssi.TrackTruck.Bussiness.Auth;
using Ssi.TrackTruck.Bussiness.DAL;
using Ssi.TrackTruck.Bussiness.DAL.Constants;
using Ssi.TrackTruck.Bussiness.DAL.Trips;

namespace Ssi.TrackTruck.Bussiness.Trips
{
    public class TripService
    {
        private readonly IRepository _repository;
        private readonly ITripRepository _tripMongoRepository;
        private readonly ISignedInUser _user;

        public TripService(IRepository repository, ITripRepository tripMongoRepository, ISignedInUser user)
        {
            _repository = repository;
            _tripMongoRepository = tripMongoRepository;
            _user = user;
        }

        public DbTrip AddTrip(TripOrderRequest orderRequest)
        {
            var trip = orderRequest.ToTrip();
            var drops = orderRequest.Drops.Select(request => request.ToDrop(trip.Id));
            
            _repository.Create(trip);            
            _repository.CreateAll(drops);

            return trip;
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

        public IQueryable<DbTripDrop> GetMyActiveDrops()
        {
            var myDrops = _tripMongoRepository.GetUsersActiveDrops(_user.Id);
            return myDrops;
        }
    }
}
