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

        public IEnumerable<Client> GetAll()
        {
            return _repository.GetAllUndeleted<Client>();
        }

        public Response Add(AddClientRequest request)
        {
            if (!request.Validate())
            {
                return Response.Error("Validation");
            }
            if (ClientNameIsDuplicate(request))
            {
                return Response.Error("", "Client with same name already exists");
            }

            if (request.Branches.Any())
            {
                if (BranchNameHasDuplicate(request))
                {
                    return Response.Error("", "Two or more branches has the same name");
                }
            }

            var branches = request.Branches.Select(b => b.ToBranch());

            var client = new Client
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
            var nameTaken = _repository.Exists<Client>(c => c.Name == request.Name);
            return nameTaken;
        }

        private static bool BranchNameHasDuplicate(AddClientRequest request)
        {
            var branchNames = request.Branches.Select(b => b.Name).ToList();
            var branchNameDuplicate = branchNames.Distinct().Count() != branchNames.Count;
            return branchNameDuplicate;
        }
        
        public Response Delete(string id)
        {
            var client = _repository.SoftDelete<Client>(id);
            if (client != null)
            {
                return Response.Success();
            }
            return Response.Error("", string.Format("Client with id '{0}' does not exist", id));
        }
    }
}
