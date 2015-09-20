using System.Collections.Generic;
using System.Linq;
using Ssi.TrackTruck.Bussiness.Auth;
using Ssi.TrackTruck.Bussiness.DAL;
using Ssi.TrackTruck.Bussiness.DAL.Clients;
using Ssi.TrackTruck.Bussiness.DAL.Entities;
using Ssi.TrackTruck.Bussiness.Models;

namespace Ssi.TrackTruck.Bussiness.Clients
{
    public class ClientService
    {
        private readonly IRepository _repository;
        private readonly AuthService _authService;

        public ClientService(IRepository repository, AuthService authService)
        {
            _repository = repository;
            _authService = authService;
        }

        public IEnumerable<Client> GetAll()
        {
            return _repository.GetAll<Client>();
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
            if (BranchNameHasDuplicate(request))
            {
                return Response.Error("", "Two or more branches has the same name");
            }
            if (BranchUsernameTaken(request))
            {
                return Response.Error("", "One or more branch usernames are already taken");
            }

            var users = request.Branches.Select(b => _authService.CreateUserObject(b.Username, b.Password, Role.BranchCustodian));
            var branches = request.Branches.Select(b => b.ToBranch());

            var client = new Client
            {
                Name = request.Name,
                TrucksPerDay = request.TrucksPerDay,
                Branches =  branches
            };

            _repository.CreateAll(users);
            _repository.Create(client);

            return Response.Success(new ClientSummary(client));
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

        private bool BranchUsernameTaken(AddClientRequest request)
        {
            var branchUsernames = request.Branches.Select(b => b.Username);
            var usernameTaken = _repository.WhereIn<User, string>(u => u.Username, branchUsernames).Any();
            return usernameTaken;
        }

        public IEnumerable<ClientSummary> GetAllSummary()
        {
            // TODO: performance, projecting all branches
            var clients = _repository
                .GetAllProjected<Client>(c => c.Id, c => c.Name, c => c.TrucksPerDay, c => c.Branches);
            var clientSummaries = clients
                .Select(c => new ClientSummary(c));

            return clientSummaries;
        }
    }
}
