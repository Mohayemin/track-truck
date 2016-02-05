using System.Web.Mvc;

namespace Ssi.TrackTruck.Web.Controllers
{
    public class PageController : ControllerBase
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