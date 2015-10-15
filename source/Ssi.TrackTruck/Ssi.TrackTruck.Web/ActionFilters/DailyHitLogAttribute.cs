using System;
using System.Web.Mvc;
using Ssi.TrackTruck.Bussiness.Attendances;
using Ssi.TrackTruck.Bussiness.Helpers;

namespace Ssi.TrackTruck.Web.ActionFilters
{
    public class DailyHitLogAttribute : ActionFilterAttribute
    {
        private readonly TimeSpan _offset;

        public DailyHitLogAttribute(TimeSpan offset)
        {
            _offset = offset;
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var identity = filterContext.HttpContext.User.Identity;
            if (!identity.IsAuthenticated)
            {
                return;
            }

            var ct = DateTimeOffset.UtcNow.ToOffset(_offset);
            var currentTime = new DateTimeModel
            {
                Year = ct.Year,
                Month = ct.Month,
                Day = ct.Day,
                Hour = ct.Hour,
                Minute = ct.Minute,
                Second = ct.Second
            };

            var attendanceService = DependencyResolver.Current.GetService<AttendanceService>();
            
            attendanceService.UpdateDailyHit(identity.Name, currentTime);
        }
    }
}