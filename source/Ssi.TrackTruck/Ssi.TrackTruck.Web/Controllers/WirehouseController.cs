using System.Web.Mvc;
using Ssi.TrackTruck.Bussiness.Wirehouses;

namespace Ssi.TrackTruck.Web.Controllers
{
    public class WirehouseController : Controller
    {
        private readonly WirehouseService _wirehouseService;

        public WirehouseController(WirehouseService wirehouseService)
        {
            _wirehouseService = wirehouseService;
        }

        [HttpGet]
        public ActionResult All()
        {
            return Json(_wirehouseService.GetAll(), JsonRequestBehavior.AllowGet);
        }
	}
}