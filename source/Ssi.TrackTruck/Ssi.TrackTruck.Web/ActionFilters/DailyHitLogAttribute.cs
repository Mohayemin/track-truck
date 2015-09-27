using System;
using System.Web.Mvc;
using Ssi.TrackTruck.Bussiness.Attendances;
using Ssi.TrackTruck.Bussiness.DAL.Constants;

namespace Ssi.TrackTruck.Web.ActionFilters
{
    public class DailyHitLogAttribute : ActionFilterAttribute
    {
        private static readonly string _sessionVariable = "0c1a5918-last-hit-time";

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var identity = filterContext.HttpContext.User.Identity;
            if (!identity.IsAuthenticated)
            {
                return;
            }

            var currentTime = DateTime.UtcNow;
            var lastLoggedHit = filterContext.HttpContext.Session[_sessionVariable] as DateTime?;
            var authService = DependencyResolver.Current.GetService<AttendanceService>();

            if (!lastLoggedHit.HasValue)
            {
                lastLoggedHit = authService.GetLastDailyHit(identity.Name);
            }

            if (LoggedForThisDay(lastLoggedHit, currentTime))
            {
                return;
            }

            filterContext.HttpContext.Session[_sessionVariable] = currentTime;
            authService.UpdateDailyHit(identity.Name, currentTime);
        }

        private static bool LoggedForThisDay(DateTime? lastLoggedHit, DateTime currentTime)
        {
            if (!lastLoggedHit.HasValue)
            {
                return false;
            }
            var currentPhilippineTime = new DateTimeOffset(currentTime).ToOffset(DateTimeConstants.PhilippineOffset);
            var loggedPhilippineTime = new DateTimeOffset(lastLoggedHit.Value).ToOffset(DateTimeConstants.PhilippineOffset);
            return currentPhilippineTime.Date == loggedPhilippineTime.Date;
        }
    }
}