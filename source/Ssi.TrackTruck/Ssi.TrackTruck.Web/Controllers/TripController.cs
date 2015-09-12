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

        [HttpPost]
        public ActionResult Order(TripOrderRequest orderRequest)
        {
            orderRequest.ClientId = User.Identity.Name;
            var trip = _tripService.AddTrip(orderRequest);

            return new JsonNetResult(trip);
        }

        [HttpGet]
        public ActionResult Order()
        {
            return View();
        }
	}
}