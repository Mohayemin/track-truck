using System.Web.Mvc;
using Ssi.TrackTruck.Bussiness.Auth;
using Ssi.TrackTruck.Bussiness.Helpers;
using Ssi.TrackTruck.Bussiness.Trips;
using Ssi.TrackTruck.Web.Auth;
using Ssi.TrackTruck.Web.Utils;

namespace Ssi.TrackTruck.Web.Controllers
{
    public class TripController : Controller
    {
        private readonly TripService _tripService;

        public TripController(TripService tripService)
        {
            _tripService = tripService;
        }

        [ValidateModel]
        [HttpPost]
        [AllowedRoles(Role.Encoder)]
        public ActionResult Order(TripOrderRequest orderRequest)
        {
            var trip = _tripService.AddTrip(orderRequest);

            return new JsonNetResult(trip);
        }

        [HttpGet]
        [AllowedRoles(Role.BranchCustodian)]
        public ActionResult MyActiveDrops()
        {
            var myActiveDrops = _tripService.GetMyActiveDrops();

            return new JsonNetResult(myActiveDrops);
        }

        [HttpPost]
        [AllowedRoles(Role.BranchCustodian)]
        public ActionResult Receive(DropReceiveRequest request)
        {
            var response = _tripService.ReceiveDrop(request);
            return new JsonNetResult(response);
        }

        [HttpPost]
        [AllowedRoles(Role.Admin)]
        public ActionResult Report(DateTimeModel fromDate, DateTimeModel toDate)
        {
            var report = _tripService.GetReport(fromDate, toDate);
            return new JsonNetResult(report);
        }
    }
}