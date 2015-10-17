using System;
using System.Web.Mvc;
using Ssi.TrackTruck.Bussiness.Attendances;

namespace Ssi.TrackTruck.Web.ActionFilters
{
    public class DailyHitLogAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var identity = filterContext.HttpContext.User.Identity;
            if (!identity.IsAuthenticated)
            {
                return;
            }

            var ct = DateTime.UtcNow;

            var attendanceService = DependencyResolver.Current.GetService<AttendanceService>();

            attendanceService.UpdateDailyHit(identity.Name, ct);
        }
    }
}