using System;
using System.Collections.Generic;
using System.Linq;
using Ssi.TrackTruck.Bussiness.Auth;
using Ssi.TrackTruck.Bussiness.DAL;
using Ssi.TrackTruck.Bussiness.DAL.Constants;
using Ssi.TrackTruck.Bussiness.DAL.Trips;
using Ssi.TrackTruck.Bussiness.Models;

namespace Ssi.TrackTruck.Bussiness.Trips
{
    public class TripService
    {
        private readonly IRepository _repository;
        private readonly ITripRepository _tripRepository;
        private readonly ISignedInUser _user;

        public TripService(IRepository repository, ITripRepository tripRepository, ISignedInUser user)
        {
            _repository = repository;
            _tripRepository = tripRepository;
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

        public TripReportResponse GetReport(DateTime fromDate, DateTime toDate)
        {
            fromDate = fromDate.ToUniversalTime();
            toDate = toDate.ToUniversalTime().AddDays(1).AddTicks(-1);
            
            var trips = _tripRepository.GetTripsInRange(fromDate, toDate);
            var drops = _tripRepository.GetDropsOfTrips(trips.Select(trip => trip.Id));

            return new TripReportResponse
            {
                FromDate = fromDate,
                ToDate = toDate,
                Trips = trips,
                Drops = drops
            };
        }

        public TripResponse Get(string id)
        {
            var trip = _repository.GetById<DbTrip>(id);
            if (trip != null)
            {
                var drops = _repository.GetWhere<DbTripDrop>(drop => drop.TripId == trip.Id);
                return new TripResponse{Trip = trip, Drops = drops};
            }

            return null;
        }
    }
}
