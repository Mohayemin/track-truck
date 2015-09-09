using Ssi.TrackTruck.Bussiness.DAL;
using Ssi.TrackTruck.Bussiness.DAL.Entities;

namespace Ssi.TrackTruck.Bussiness.Trips
{
    public class TripService
    {
        private readonly IRepository _repository;

        public TripService(IRepository repository)
        {
            _repository = repository;
        }

        public Trip AddTrip(AddTripRequest request)
        {
            var trip = request.ToTrip();
            return _repository.Create(trip);
        }
    }
}
