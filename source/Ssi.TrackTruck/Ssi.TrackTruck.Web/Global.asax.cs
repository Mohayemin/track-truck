using System.Collections.Generic;
using System.Web.Mvc;
using System.Web.Routing;
using Ssi.TrackTruck.Bussiness.DAL.Constants;
using Ssi.TrackTruck.Web.ActionFilters;
using Ssi.TrackTruck.Web.Utils;

namespace Ssi.TrackTruck.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            ModelBinders.Binders.Add(typeof(IList<string>), new ListModelBinder<string>());
            GlobalFilters.Filters.Add(new DailyHitLogAttribute(DateTimeConstants.PhilippineOffset));
        }
    }
}
