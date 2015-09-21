using System.Web.Mvc;
using Ssi.TrackTruck.Bussiness.Warehouses;

namespace Ssi.TrackTruck.Web.Controllers
{
    public class WarehouseController : Controller
    {
        private readonly WarehouseService _warehouseService;

        public WarehouseController(WarehouseService warehouseService)
        {
            _warehouseService = warehouseService;
        }

        [HttpGet]
        public ActionResult All()
        {
            return Json(_warehouseService.GetAll(), JsonRequestBehavior.AllowGet);
        }
	}
}