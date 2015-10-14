using System.Web.Mvc;
using Ssi.TrackTruck.Bussiness.Helpers;
using Ssi.TrackTruck.Bussiness.Trips;
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

        public ActionResult All()
        {
            return Json(_tripService.GetAll(), JsonRequestBehavior.AllowGet);
        }

        [ValidateModel]
        [HttpPost]
        public ActionResult Order(TripOrderRequest orderRequest)
        {
            var trip = _tripService.AddTrip(orderRequest);

            return new JsonNetResult(trip);
        }

        [HttpGet]
        public ActionResult Active()
        {
            var activeTrips = _tripService.GetActiveTrips();

            return new JsonNetResult(activeTrips);
        }

        [HttpGet]
        public ActionResult MyActiveDrops()
        {
            var myActiveDrops = _tripService.GetMyActiveDrops();

            return new JsonNetResult(myActiveDrops);
        }

        [HttpPost]
        public ActionResult Receive(DropReceiveRequest request)
        {
            var response = _tripService.ReceiveDrop(request);
            return new JsonNetResult(response);
        }

        [HttpPost]
        public ActionResult Report(DateTimeModel fromDate, DateTimeModel toDate)
        {
            return new JsonNetResult(new {fromDate, toDate});
        }
	}
}