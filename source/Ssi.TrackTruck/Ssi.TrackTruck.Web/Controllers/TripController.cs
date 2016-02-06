using System;
using System.Web.Mvc;
using Ssi.TrackTruck.Bussiness.Auth;
using Ssi.TrackTruck.Bussiness.DAL.Constants;
using Ssi.TrackTruck.Bussiness.Trips;
using Ssi.TrackTruck.Web.Auth;
using Ssi.TrackTruck.Web.Utils;

namespace Ssi.TrackTruck.Web.Controllers
{
    public class TripController : ControllerBase
    {
        private readonly TripService _tripService;

        public TripController(TripService tripService)
        {
            _tripService = tripService;
        }

        [ValidateModel]
        [HttpPost]
        [AllowedRoles(Role.Encoder, Role.Admin)]
        public ActionResult Order(TripOrderRequest orderRequest)
        {
            var trip = _tripService.AddTrip(orderRequest);

            return JsonNet(trip);
        }

        [HttpPost]
        [AllowedRoles(Role.Encoder, Role.Admin)]
        public ActionResult Report(DateTime fromDate, DateTime toDate)
        {
            var report = _tripService.GetReport(fromDate, toDate);
            return JsonNet(report);
        }

        [HttpGet]
        public ActionResult Get(string id)
        {
            var trip = _tripService.Get(id);
            return JsonNet(trip);
        }

        [HttpGet]
        [AllowedRoles(Role.Encoder, Role.Admin)]
        public ActionResult GetActiveTrips()
        {
            var trips = _tripService.GetActiveTrips();
            return JsonNet(trips);
        }

        [HttpPost]
        public ActionResult UpdateStatus(string tripId, TripStatus status)
        {
            _tripService.UpdateStatus(tripId, status);
            return JsonNet(new { success = true });
        }
    }
}