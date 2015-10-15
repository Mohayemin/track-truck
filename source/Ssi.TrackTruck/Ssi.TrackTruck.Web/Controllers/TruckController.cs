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

        [HttpPost]
        public ActionResult Delete(string id)
        {
            var response = _truckService.Delete(id);
            return Json(response);
        }

        [HttpPost]
        public ActionResult Save(EditTruckRequest request)
        {
            var response = _truckService.Save(request);
            return Json(response);
        }
    }
}