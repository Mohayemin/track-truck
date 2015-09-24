using System.Web.Mvc;
using System.Web.Security;
using Ssi.TrackTruck.Bussiness.Auth;
using Ssi.TrackTruck.Bussiness.Models;

namespace Ssi.TrackTruck.Web.Controllers
{
    public class AuthController : Controller
    {
        private readonly AuthService _authService;

        public AuthController(AuthService authService)
        {
            _authService = authService;
        }
        
        [HttpPost]
        public ActionResult SignIn(SignInRequest request)
        {
            var response = _authService.AuthenticateUser(request);
            if (!response.IsError)
            {
                FormsAuthentication.SetAuthCookie(request.Username, request.RememberMe);
            }
            return Json(response);
        }

        public ActionResult SignOut()
        {
            FormsAuthentication.SignOut();
            return Redirect(Url.Content("~/"));
        }
        
        [HttpPost]
        public ActionResult ChangePassword(ChangePasswordRequest request)
        {
            var response = _authService.ChangePassword(request, User.Identity.Name);
            return Json(response);
        }
    }
}