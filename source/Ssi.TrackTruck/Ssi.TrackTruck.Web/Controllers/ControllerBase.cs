using System.Web.Mvc;
using Ssi.TrackTruck.Web.Utils;

namespace Ssi.TrackTruck.Web.Controllers
{
    public class ControllerBase : Controller
    {
        protected JsonResult JsonNet(object data)
        {
            return new JsonNetResult(data);
        }
    }
}