using System.Web.Mvc;
using Ssi.TrackTruck.Bussiness.Attendances;
using Ssi.TrackTruck.Bussiness.Helpers;

namespace Ssi.TrackTruck.Web.Controllers
{
    public class AttendanceController : Controller
    {
        private readonly AttendanceService _attendanceService;

        public AttendanceController(AttendanceService attendanceService)
        {
            _attendanceService = attendanceService;
        }

        [HttpPost]
        public ActionResult Report(DateTimeModel fromDate, DateTimeModel toDate)
        {
            var report = _attendanceService.GetReport(fromDate, toDate);
            return Json(report, JsonRequestBehavior.AllowGet);
        }
    }
}