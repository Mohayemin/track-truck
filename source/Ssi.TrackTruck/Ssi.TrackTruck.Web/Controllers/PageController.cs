using System.Web.Mvc;

namespace Ssi.TrackTruck.Web.Controllers
{
    public class PageController : Controller
    {
        [Route("")]
        public ActionResult App()
        {
            if (User.Identity.IsAuthenticated)
            {
                return View("App");
            }
            return View("SignIn");
        }
	}
}