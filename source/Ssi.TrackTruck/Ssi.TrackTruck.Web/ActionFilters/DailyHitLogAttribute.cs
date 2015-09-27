using System;
using System.Web.Mvc;
using Ssi.TrackTruck.Bussiness.DAL.Constants;

namespace Ssi.TrackTruck.Web.ActionFilters
{
    public class DailyHitLogAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (filterContext.HttpContext.User.Identity.IsAuthenticated)
            {
                var currentTime = DateTimeOffset.UtcNow.ToOffset(DateTimeConstants.PhilippineOffset);
                var lastLoggedHit = filterContext.HttpContext.Session["last-hit-time"] as DateTime?;
                if (lastLoggedHit.HasValue && currentTime.Date == lastLoggedHit.Value.Date)
                {
                    // already logged for today
                }
                else
                {
                    filterContext.HttpContext.Session["last-hit-time"] = currentTime.DateTime;
                }
            }
            base.OnActionExecuting(filterContext);
        }
    }
}