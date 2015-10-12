using System.Collections.Generic;
using System.Linq;
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

        public IEnumerable<DbClient> GetAll()
        {
            return _repository.GetAllUndeleted<DbClient>();
        }

        public Response Add(AddClientRequest request)
        {
            if (ClientNameIsDuplicate(request))
            {
                return Response.DuplicacyError("Client with same name already exists");
            }
            
            var branches = request.Branches.Select(b => b.ToBranch());

            var client = new DbClient
            {
                Name = request.Name,
                TrucksPerDay = request.TrucksPerDay,
                Branches = branches
            };

            _repository.Create(client);

            return Response.Success(client);
        }

        private bool ClientNameIsDuplicate(AddClientRequest request)
        {
            var nameTaken = _repository.Exists<DbClient>(c => c.Name == request.Name);
            return nameTaken;
        }

        
        
        public Response Delete(string id)
        {
            var client = _repository.SoftDelete<DbClient>(id);
            if (client != null)
            {
                return Response.Success(null, "Successfully deleted");
            }
            return Response.Error("", string.Format("The client you tried to delete does not exist"));
        }

        public DbClient GetClient(string clientId)
        {
            return GetAll().FirstOrDefault(client => client.Id == clientId);
        }
    }
}
