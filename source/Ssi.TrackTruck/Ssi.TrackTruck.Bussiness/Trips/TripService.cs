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
        private readonly ISignedInUser _signedInUser;

        public TripService(IRepository repository, ITripRepository tripRepository, ISignedInUser signedInUser)
        {
            _repository = repository;
            _tripRepository = tripRepository;
            _signedInUser = signedInUser;
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
                return new TripResponse { Trip = trip, Drops = drops };
            }

            return null;
        }

        public IEnumerable<TripResponse> GetActiveTrips()
        {
            var trips = _repository
                .GetWhere<DbTrip>(trip => trip.Status == TripStatus.InProgress || trip.Status == TripStatus.New)
                .OrderBy(trip => trip.Status);

            var tripIds = trips.Select(trip => trip.Id);

            var tripMap = trips.ToDictionary(trip => trip.Id, trip => trip);

            var allDrops = _repository.WhereIn<DbTripDrop, string>(drop => drop.TripId, tripIds)
                .GroupBy(drop => drop.TripId);

            foreach (var dropGroup in allDrops)
            {
                yield return new TripResponse { Drops = dropGroup, Trip = tripMap[dropGroup.Key] };
            }
        }

        public void UpdateStatus(string tripId, TripStatus status)
        {
            var trip = _repository.GetById<DbTrip>(tripId);
            trip.Status = status;
            _repository.Save(trip);
        }

        public Response ReceiveDrop(DropReceiveRequest request)
        {
            var drop = _tripRepository.GetDrop(request.DropId);

            if (drop == null)
            {
                return Response.Error("", "Drop not found");
            }
            if (drop.IsDelivered)
            {
                return Response.Error("", "The drop is already received");
            }

            foreach (var rejection in request.DeliveryRejections)
            {
                var dr = drop.DeliveryReceipts.FirstOrDefault(_ => _.Id == rejection.Key);

                if (dr == null)
                {
                    return Response.Error("", "Request contains a DR that does not exist");
                }
                if (rejection.Value > dr.NumberOfBoxes)
                {
                    return Response.Error("", string.Format("{0} boxes rejected but total number of boxes is {1}", rejection.Value, dr.NumberOfBoxes));
                }
                if (rejection.Value < 0)
                {
                    return Response.Error("", "Cannot reject negetive number of boxes");
                }

                dr.RejectedNumberOfBoxes = rejection.Value;
            }

            drop.ActualDropTimeUtc = DateTime.UtcNow;
            drop.ReceiverUserId = _signedInUser.Id;
            drop.IsDelivered = true;

            _repository.Save(drop);

            var trip = _repository.GetById<DbTrip>(drop.TripId);
            var allDelivered = _repository.Exists<DbTripDrop>(d => !d.IsDelivered);
            TripStatus newStatus;
            if (allDelivered)
            {
                newStatus = _repository.Exists<DbTripDrop>(d => d.DeliveryReceipts.Any(dr => dr.RejectedNumberOfBoxes == 0)) ?
                    TripStatus.DoneWithPartialDelivery : TripStatus.DoneWithFullDelivery;
            }
            else
            {
                newStatus = TripStatus.InProgress;
            }
            if (newStatus != trip.Status)
            {
                trip.Status = newStatus;
                _repository.Save(trip);
            }

            return Response.Success(drop, "Drop received");
        }
    }
}
