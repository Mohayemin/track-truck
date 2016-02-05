using System.Web.Mvc;
using Ssi.TrackTruck.Bussiness.Auth;
using Ssi.TrackTruck.Bussiness.Trucks;
using Ssi.TrackTruck.Web.Auth;

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
        [AllowedRoles(Role.Admin, Role.Encoder)]
        public ActionResult All()
        {
            return Json(_truckService.GetAll(), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [AllowedRoles(Role.Admin)]
        public ActionResult Add(AddTruckRequest request)
        {
            return Json(_truckService.Add(request));
        }

        [HttpPost]
        [AllowedRoles(Role.Admin)]
        public ActionResult Delete(string id)
        {
            var response = _truckService.Delete(id);
            return Json(response);
        }

        [HttpPost]
        [AllowedRoles(Role.Admin)]
        public ActionResult Save(EditTruckRequest request)
        {
            var response = _truckService.Save(request);
            return Json(response);
        }
    }
}