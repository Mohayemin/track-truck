using System.Collections.Generic;
using Ssi.TrackTruck.Bussiness.DAL;
using Ssi.TrackTruck.Bussiness.DAL.Clients;
using Ssi.TrackTruck.Bussiness.Models;

namespace Ssi.TrackTruck.Bussiness.Clients
{
    public class ClientService
    {
        private readonly IRepository _repository;

        public ClientService(IRepository repository)
        {
            _repository = repository;
        }

        public IEnumerable<Client> GetAll()
        {
            return _repository.GetAll<Client>();
        }

        public Response Add(AddClientRequest request)
        {
            if (request.Validate())
            {
                var client = _repository.Create(request.ToClient());
                return Response.Success(client);
            }

            return Response.Error("Validation");
        }
    }
}
