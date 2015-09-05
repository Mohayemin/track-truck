using System.Web.Mvc;

namespace Ssi.TrackTruck.Web.Controllers
{
    public class TruckController : Controller
    {
        public ActionResult Report()
        {
            return View();
        }

        public ActionResult GetCurrentStatus()
        {
            var data = System.IO.File.ReadAllText(@"F:\UpWork\track-truck\data\truck-status.json");

            return Content(data, "Application/Json");
        }
	}
}