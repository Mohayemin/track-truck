using System.Web.Mvc;

namespace Ssi.TrackTruck.Web.Controllers
{
    public class PageController : Controller
    {
        [Route("")]
        public ActionResult App()
        {
            return View();
        }

        [Route("signin")]
        public ActionResult SignIn()
        {
            return View();
        }
	}
}