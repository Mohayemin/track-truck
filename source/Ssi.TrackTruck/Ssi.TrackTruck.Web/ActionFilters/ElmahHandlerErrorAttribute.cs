using System.Web.Mvc;
using Elmah;

namespace Ssi.TrackTruck.Web.ActionFilters
{
    public class ElmahHandleErrorAttribute : HandleErrorAttribute
    {
        public override void OnException(ExceptionContext filterContext)
        {
            var exceptionHandled = filterContext.ExceptionHandled;

            base.OnException(filterContext);

            if (!exceptionHandled && filterContext.ExceptionHandled)
                ErrorSignal.FromCurrentContext().Raise(filterContext.Exception);
        }
    }
}