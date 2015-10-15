using System.Web;
using System.Web.Mvc;

namespace Ssi.TrackTruck.Web.Utils
{
    public class TrackTruckAuthorizeAttribute : AuthorizeAttribute 
    {
        public bool AdminOnly { get; set; }
        public bool StoreOnly { get; set; }

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            var authorized = base.AuthorizeCore(httpContext);

            if (authorized)
            {
                
            }

            return authorized;
        }
    }
}
