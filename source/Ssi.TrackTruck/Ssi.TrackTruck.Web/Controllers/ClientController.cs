using System.Web.Mvc;
using Ssi.TrackTruck.Bussiness.Auth;
using Ssi.TrackTruck.Bussiness.Clients;
using Ssi.TrackTruck.Web.Auth;
using Ssi.TrackTruck.Web.Utils;

namespace Ssi.TrackTruck.Web.Controllers
{
    public class ClientController : ControllerBase
    {
        private readonly ClientService _clientService;

        public ClientController(ClientService clientService)
        {
            _clientService = clientService;
        }

        [HttpGet]
        [AllowedRoles(Role.Admin, Role.Encoder)]
        public ActionResult All()
        {
            return JsonNet(_clientService.GetAll());
        }

        [ValidateModel]
        [HttpPost]
        [AllowedRoles(Role.Admin)]
        public ActionResult Add(AddClientRequest request)
        {
            var response = _clientService.Add(request);
            return JsonNet(response);
        }

        [HttpPost]
        [AllowedRoles(Role.Admin)]
        public ActionResult Delete(string id)
        {
            var response = _clientService.Delete(id);
            return JsonNet(response);
        }

        [HttpPost]
        [AllowedRoles(Role.Admin)]
        public ActionResult Edit(EditClientRequest request)
        {
            var response = _clientService.Edit(request);
            return JsonNet(response);
        }
    }
}