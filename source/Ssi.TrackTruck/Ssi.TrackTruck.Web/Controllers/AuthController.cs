using System.Web.Mvc;
using System.Web.Security;
using Ssi.TrackTruck.Bussiness.Auth;
using Ssi.TrackTruck.Bussiness.DAL.Users;
using Ssi.TrackTruck.Bussiness.Models;
using Ssi.TrackTruck.Web.Utils;
using static Ssi.TrackTruck.Web.Utils.JsonNetResult;

namespace Ssi.TrackTruck.Web.Controllers
{
    public class AuthController : Controller
    {
        private readonly AuthService _authService;

        public AuthController(AuthService authService)
        {
            _authService = authService;
        }
        
        [ValidateModel]
        [HttpPost]
        public ActionResult SignIn(SignInRequest request)
        {
            DbUser user;
            var response = _authService.AuthenticateUser(request, out user);
            if (!response.IsError)
            {
                FormsAuthentication.SetAuthCookie(user.Id, request.RememberMe);
            }
            return JsonNet(response);
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
            return JsonNet(response);
        }

        [HttpGet]
        public ActionResult GetSignedInUser()
        {
            var user = _authService.GetUser(User.Identity.Name);
            return JsonNet(user);
        }
    }
}