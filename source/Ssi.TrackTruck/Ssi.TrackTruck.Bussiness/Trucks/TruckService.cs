using System.Collections.Generic;
using System.Linq;
using Ssi.TrackTruck.Bussiness.DAL;
using Ssi.TrackTruck.Bussiness.DAL.Entities;
using Ssi.TrackTruck.Bussiness.DAL.Trips;
using Ssi.TrackTruck.Bussiness.Helpers;
using Ssi.TrackTruck.Bussiness.Models;

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
                _repository.GetAll<DbTruck>();

            var tripIds = allTrucks.Select(truck => truck.CurrentTripId);

            var tripsById =
                _repository.WhereIn<DbTrip, string>(trip => trip.Id, tripIds)
                    .ToDictionary(trip => trip.Id, trip => trip);

            var defaultTrip = new DbTrip();

            return allTrucks.Select(truck => new TruckStatusItem(truck, tripsById.GetOrDefault(truck.CurrentTripId) ?? defaultTrip));
        }

        public Response Add(AddTruckRequest request)
        {
            var validation = request.Validate();
            if (validation.IsError)
            {
                return validation;
            }
            if (_repository.Exists<DbTruck>(dbTruck => dbTruck.RegistrationNumber == request.RegistrationNumber))
            {
                return Response.DuplicacyError("A truck with this registration number already exists");
            }
            var truck = _repository.Create(request.ToTruck());
            return Response.Success(truck);
        }

        public IEnumerable<DbTruck> GetAll()
        {
            return _repository.GetAll<DbTruck>();
        }
    }
}
