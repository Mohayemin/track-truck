using System.Linq;
using System.Net;
using System.Web.Mvc;
using Ssi.TrackTruck.Bussiness.Auth;

namespace Ssi.TrackTruck.Web.Auth
{
    public class AllowedRoles : ActionFilterAttribute
    {
        private readonly Role[] _allowedRoles;

        public AllowedRoles(params Role[] allowedRoles)
        {
            _allowedRoles = allowedRoles;
        }

        private bool Allow()
        {
            var user = DependencyResolver.Current.GetService<ISignedInUser>();
            if (!user.IsSignedIn)
            {
                return false;
            }
            if (_allowedRoles == null)
            {
                return true;
            }
            var authService = DependencyResolver.Current.GetService<AuthService>();
            var dbUser = authService.GetUser(user.Id);
            return _allowedRoles.Contains(dbUser.Role);
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (!Allow())
            {
                filterContext.HttpContext.Response.SuppressFormsAuthenticationRedirect = true;
                filterContext.Result = new HttpStatusCodeResult(HttpStatusCode.Unauthorized);
            }
        }
    }
}
