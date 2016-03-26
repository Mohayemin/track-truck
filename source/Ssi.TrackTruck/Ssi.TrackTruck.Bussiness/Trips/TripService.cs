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
            var contracts = orderRequest.ToContracts(trip.Id);

            _repository.Create(trip);
            _repository.CreateAll(drops);
            _repository.CreateAll(contracts);

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

            var tripResponses = GetTripResponses(trips);

            return new TripReportResponse
            {
                FromDate = fromDate,
                ToDate = toDate,
                Trips = tripResponses
            };
        }

        private IEnumerable<TripResponse> GetTripResponses(IQueryable<DbTrip> trips)
        {
            var tripIds = trips.Select(trip => trip.Id).ToList();

            var drops = _tripRepository.GetIndexedDrops(tripIds);
            var contracts = _tripRepository.GetIndexedContracts(tripIds);

            foreach (var dbTrip in trips)
            {
                yield return new TripResponse(dbTrip, drops[dbTrip.Id], contracts[dbTrip.Id]);
            }
        }


        public TripResponse Get(string id)
        {
            var trip = _repository.GetById<DbTrip>(id);
            if (trip != null)
            {
                var drops = _repository.GetWhere<DbTripDrop>(drop => drop.TripId == trip.Id);
                var contracts = _repository.GetWhere<DbTripContract>(contract => contract.TripId == trip.Id);
                return new TripResponse(trip, drops, contracts);
            }

            return null;
        }

        public IEnumerable<TripResponse> GetActiveTrips()
        {
            var trips = _repository
                .WhereIn<DbTrip, TripStatus>(trip => trip.Status, new[] { TripStatus.InProgress, TripStatus.New, TripStatus.Delivered })
                .OrderBy(trip => trip.Status);

            var tripIds = trips.Select(trip => trip.Id);

            var dropIndex = _repository.WhereIn<DbTripDrop, string>(drop => drop.TripId, tripIds)
                .GroupBy(drop => drop.TripId).ToDictionary(g => g.Key, g => g.ToList());

            var contractIndex = _repository.WhereIn<DbTripContract, string>(contract => contract.TripId, tripIds)
                .GroupBy(contract => contract.TripId).ToDictionary(g => g.Key, g => g.ToList());

            return trips.Select(trip => new TripResponse(trip, dropIndex[trip.Id], contractIndex[trip.Id]));
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

            foreach (var rejection in request.DeliveryRejections)
            {
                var dr = drop.DeliveryReceipts.FirstOrDefault(_ => _.Id == rejection.DeliveryReceiptId);

                if (dr == null)
                {
                    return Response.Error("", "Request contains a DR that does not exist");
                }
                if (rejection.RejectedNumberOfBoxes > dr.NumberOfBoxes)
                {
                    return Response.Error("", string.Format("{0} boxes rejected but total number of boxes is {1}", rejection.RejectedNumberOfBoxes, dr.NumberOfBoxes));
                }
                if (rejection.RejectedNumberOfBoxes < 0)
                {
                    return Response.Error("", "Cannot reject negetive number of boxes");
                }

                dr.RejectedNumberOfBoxes = rejection.RejectedNumberOfBoxes;
                dr.Comment = rejection.Comment;
            }

            drop.ActualDropTimeUtc = DateTime.UtcNow;
            drop.ReceiverUserId = _signedInUser.Id;
            drop.IsDelivered = true;

            _repository.Save(drop);

            var trip = _repository.GetById<DbTrip>(drop.TripId);
            var thisTripDrops = _repository.GetWhere<DbTripDrop>(d => d.TripId == drop.TripId);
            var notAllDelivered = thisTripDrops.Any(d => !d.IsDelivered);
            var newStatus = notAllDelivered ? TripStatus.InProgress : TripStatus.Delivered;
            if (newStatus != trip.Status)
            {
                trip.Status = newStatus;
                _repository.Save(trip);
            }

            return Response.Success(trip.Status.ToString(), "Drop received");
        }

        public Response SaveAdjustments(string tripId, List<CostAdjustment> adjustments)
        {
            var trip = _repository.GetById<DbTrip>(tripId);
            if (trip == null)
            {
                return Response.Error("", "No such trip found");
            }

            var updatedAdjustments = adjustments.Where(adjustment => adjustment.Id != null);
            var newAdjustments = adjustments.Where(adjustment => adjustment.Id == null);

            var costs = trip.Costs;

            var updatedIds = updatedAdjustments.Select(adjustment => adjustment.Id).ToList();

            costs.RemoveAll(cost => !updatedIds.Contains(cost.Id));

            costs.ForEach(cost =>
            {
                var adjustment = adjustments.Find(a => a.Id == cost.Id);
                cost.ActualCostInPeso = adjustment.ActualCostInPeso;
                cost.Comment = adjustment.Comment;
            });

            costs.AddRange(newAdjustments.Select(a => new DbTripCost(TripCostType.Discrepancy, 0, a.ActualCostInPeso, a.Comment)));

            _repository.Save(trip);
            return Response.Success(message: "Adjustments saved");
        }
    }
}
