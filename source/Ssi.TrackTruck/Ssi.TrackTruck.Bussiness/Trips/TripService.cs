using System;
using System.Collections.Generic;
using System.Linq;
using MongoDB.Driver.Linq;
using Ssi.TrackTruck.Bussiness.Auth;
using Ssi.TrackTruck.Bussiness.DAL;
using Ssi.TrackTruck.Bussiness.DAL.Constants;
using Ssi.TrackTruck.Bussiness.DAL.Trips;
using Ssi.TrackTruck.Bussiness.Helpers;
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

        public IQueryable<DbTripDrop> GetMyActiveDrops()
        {
            var myDrops = _tripRepository.GetUsersActiveDrops(_user.Id);
            return myDrops;
        }

        public Response ReceiveDrop(DropReceiveRequest request)
        {
            var drop = _tripRepository.GetDrop(request.DropId);

            if (drop == null)
            {
                return Response.Error("", "Drop not found");
            }
            if (drop.IsReceived)
            {
                return Response.Error("", "The drop is already received");
            }
            var myBranches = _tripRepository.GetUserBranchIds(_user.Id);
            if (!myBranches.Contains(drop.BranchId))
            {
                return Response.Error("", "You are not custodian of the branch of this drop");
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

            drop.ActualDropTime = DateTime.UtcNow;
            drop.ReceiverUserId = _user.Id;
            drop.IsReceived = true;

            _repository.Save(drop);

            return Response.Success(drop, "Drop received");
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

    }
}
