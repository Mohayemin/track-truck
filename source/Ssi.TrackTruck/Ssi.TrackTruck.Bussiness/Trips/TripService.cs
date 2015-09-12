using Ssi.TrackTruck.Bussiness.DAL;
using Ssi.TrackTruck.Bussiness.DAL.Trips;

namespace Ssi.TrackTruck.Bussiness.Trips
{
    public class TripService
    {
        private readonly IRepository _repository;

        public TripService(IRepository repository)
        {
            _repository = repository;
        }

        public Trip AddTrip(TripOrderRequest orderRequest)
        {
            var trip = orderRequest.ToTrip();
            return _repository.Create(trip);
        }
    }
}
