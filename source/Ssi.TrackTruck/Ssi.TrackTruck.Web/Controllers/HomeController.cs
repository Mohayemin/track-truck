using System.Web.Mvc;
using Ssi.TrackTruck.Web.Utils;

namespace Ssi.TrackTruck.Web.Controllers
{
    public class HomeController : Controller
    {
        [TrackTruckAuthorize]
        public ActionResult Index()
        {
            return View();
        }
	}
}