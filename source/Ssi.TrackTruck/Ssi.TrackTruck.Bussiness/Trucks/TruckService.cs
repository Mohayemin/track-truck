using System.Collections.Generic;
using System.Linq;
using Ssi.TrackTruck.Bussiness.DAL;
using Ssi.TrackTruck.Bussiness.DAL.Entities;

namespace Ssi.TrackTruck.Bussiness.Trucks
{
    public class TruckService
    {
        private readonly IRepository _repository;

        public TruckService(IRepository repository)
        {
            _repository = repository;
        }

        public IEnumerable<TruckStatusItem> GetCurrentStatus()
        {
            var allTrucks =
                _repository.GetAll<Truck>();

            var tripIds = allTrucks.Select(truck => truck.CurrentTripId);

            var tripsById =
                _repository.WhereIn<Trip, string>(trip => trip.Id, tripIds)
                    .ToDictionary(trip => trip.Id, trip => trip);

            return allTrucks.Select(truck => new TruckStatusItem(truck, tripsById[truck.CurrentTripId]));
        }
    }
}
