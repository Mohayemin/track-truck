using System.Web.Mvc;
using Ssi.TrackTruck.Bussiness.Clients;

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
        
        [HttpPost]
        public ActionResult Add(AddClientRequest request)
        {
            var response = _clientService.Add(request);
            return Json(response);
        }
	}
}