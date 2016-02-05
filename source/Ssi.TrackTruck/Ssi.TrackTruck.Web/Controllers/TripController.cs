using System;
using System.Web.Mvc;
using Ssi.TrackTruck.Bussiness.Auth;
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
    }
}