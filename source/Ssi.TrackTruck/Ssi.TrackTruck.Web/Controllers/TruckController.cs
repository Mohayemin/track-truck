using System.Web.Mvc;
using Ssi.TrackTruck.Bussiness.Trucks;

namespace Ssi.TrackTruck.Web.Controllers
{
    public class TruckController : Controller
    {
        private readonly TruckService _truckService;

        public TruckController(TruckService truckService)
        {
            _truckService = truckService;
        }
        
        public ActionResult GetCurrentStatus()
        {
            var data = _truckService.GetCurrentStatus();

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult All()
        {
            return Json(_truckService.GetAll(), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Add(AddTruckRequest request)
        {
            return Json(_truckService.Add(request));
        }
    }
}