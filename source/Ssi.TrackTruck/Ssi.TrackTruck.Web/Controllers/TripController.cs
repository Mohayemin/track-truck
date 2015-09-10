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
        public ActionResult Add(AddTripRequest request)
        {
            request.ClientId = User.Identity.Name;
            var trip = _tripService.AddTrip(request);

            return new JsonNetResult(trip);
        }

        public ActionResult Index()
        {
            return View();
        }
	}
}