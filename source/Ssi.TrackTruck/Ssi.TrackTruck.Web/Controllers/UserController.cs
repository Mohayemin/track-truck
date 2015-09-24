using System.Web.Mvc;
using Ssi.TrackTruck.Bussiness.Auth;

namespace Ssi.TrackTruck.Web.Controllers
{
    public class UserController : Controller
    {
        private readonly AuthService _authService;

        public UserController(AuthService authService)
        {
            _authService = authService;
        }

        [HttpPost]
        public ActionResult Add(AddUserRequest request)
        {
            var response = _authService.CreateUser(request);
            return Json(response);
        }

        [HttpGet]
        public ActionResult All()
        {
            var users = _authService.GetUserList();
            return Json(users, JsonRequestBehavior.AllowGet);
        }
	}
}