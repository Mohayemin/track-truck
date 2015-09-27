using System.Web.Mvc;
using Ssi.TrackTruck.Bussiness.Helpers;

namespace Ssi.TrackTruck.Web.Controllers
{
    public class AttendanceController : Controller
    {
        [HttpPost]
        public ActionResult Report(DateTimeModel fromDate, DateTimeModel toDate)
        {
            return Json(new { fromDate, toDate }, JsonRequestBehavior.AllowGet);
        }
    }
}