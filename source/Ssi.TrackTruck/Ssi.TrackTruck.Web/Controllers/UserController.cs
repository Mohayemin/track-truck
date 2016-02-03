using System.Web.Mvc;
using Ssi.TrackTruck.Bussiness.Auth;
using Ssi.TrackTruck.Web.Auth;
using Ssi.TrackTruck.Web.Utils;

namespace Ssi.TrackTruck.Web.Controllers
{
    public class UserController : Controller
    {
        private readonly AuthService _authService;

        public UserController(AuthService authService)
        {
            _authService = authService;
        }

        [ValidateModel]
        [HttpPost]
        [AllowedRoles(Role.Admin)]
        public ActionResult Add(AddUserRequest request)
        {
            var response = _authService.CreateUser(request);
            return Json(response);
        }

        [HttpGet]
        [AllowedRoles(Role.Admin, Role.Encoder)]
        public ActionResult All()
        {
            var users = _authService.GetUserList();
            return Json(users, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [AllowedRoles(Role.Admin)]
        public ActionResult Delete(string id)
        {
            var response = _authService.Delete(id, User.Identity.Name);
            return Json(response);
        }

        [HttpPost]
        [AllowedRoles(Role.Admin)]
        public ActionResult Save(EditUserRequest request)
        {
            var response = _authService.Save(request);
            return Json(response);
        }
	}
}