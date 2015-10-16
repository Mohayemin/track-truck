using System.Web.Mvc;
using Ssi.TrackTruck.Bussiness.Attendances;
using Ssi.TrackTruck.Bussiness.Auth;
using Ssi.TrackTruck.Bussiness.Helpers;
using Ssi.TrackTruck.Web.Auth;

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
        [AllowedRoles(Role.Admin)]
        public ActionResult Report(DateTimeModel fromDate, DateTimeModel toDate)
        {
            var report = _attendanceService.GetReport(fromDate, toDate);
            return Json(report, JsonRequestBehavior.AllowGet);
        }
    }
}