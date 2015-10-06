using System.Web.Mvc;
using System.Web.Security;
using Ssi.TrackTruck.Bussiness.Auth;
using Ssi.TrackTruck.Bussiness.DAL.Users;
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
            DbUser user;
            var response = _authService.AuthenticateUser(request, out user);
            if (!response.IsError)
            {
                FormsAuthentication.SetAuthCookie(user.Id, request.RememberMe);
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

        [HttpGet]
        public ActionResult GetSignedInUser()
        {
            var user = _authService.GetUser(User.Identity.Name);
            return Json(user, JsonRequestBehavior.AllowGet);
        }
    }
}