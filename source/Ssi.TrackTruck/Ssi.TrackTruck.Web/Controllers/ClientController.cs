using System.Web.Mvc;
using Ssi.TrackTruck.Bussiness.Clients;
using Ssi.TrackTruck.Web.Utils;

namespace Ssi.TrackTruck.Web.Controllers
{
    public class ClientController : Controller
    {
        private readonly ClientService _clientService;

        public ClientController(ClientService clientService)
        {
            _clientService = clientService;
        }

        [HttpGet]
        public ActionResult All()
        {
            return Json(_clientService.GetAll(), JsonRequestBehavior.AllowGet);
        }

        [ValidateModel]
        [HttpPost]
        public ActionResult Add(AddClientRequest request)
        {
            var response = _clientService.Add(request);
            return Json(response);
        }
        [HttpPost]
        public ActionResult Delete(string id)
        {
            var response = _clientService.Delete(id);
            return Json(response);
        }
    }
}