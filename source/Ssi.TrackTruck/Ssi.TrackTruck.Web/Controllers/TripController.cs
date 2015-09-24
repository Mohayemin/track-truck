using System.Linq;
using System.Web.Mvc;
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

        [HttpPost]
        public ActionResult Order(TripOrderRequest orderRequest)
        {
            var trip = _tripService.AddTrip(orderRequest);

            return new JsonNetResult(trip);
        }
	}
}