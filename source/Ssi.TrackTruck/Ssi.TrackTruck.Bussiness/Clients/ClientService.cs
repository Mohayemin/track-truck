﻿using System;
using System.Collections.Generic;
using System.Linq;
using Ssi.TrackTruck.Bussiness.Auth;
using Ssi.TrackTruck.Bussiness.DAL;
using Ssi.TrackTruck.Bussiness.DAL.Clients;
using Ssi.TrackTruck.Bussiness.Helpers;
using Ssi.TrackTruck.Bussiness.Models;

namespace Ssi.TrackTruck.Bussiness.Clients
{
    public class ClientService
    {
        private readonly IRepository _repository;
        private readonly ISignedInUser _user;

        public ClientService(IRepository repository, ISignedInUser user)
        {
            _repository = repository;
            _user = user;
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
            
            var dbClient = request.ToDbClient();

            _repository.Create(dbClient);

            return Response.Success(dbClient);
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

        public Response Edit(EditClientRequest request)
        {
            var client = _repository.GetById<DbClient>(request.Id);

            var deletedBrancheIds = request.Branches.Where(branch => branch.ModificationStatus.HasFlag(CrudStatus.Deleted)).Select(branch => branch.Id).ToList();
            var deletedDbBranches = client.Branches.Where(branch => deletedBrancheIds.Contains(branch.Id));

            client.Branches =
                request.Branches.Where(branch => !branch.ModificationStatus.HasFlag(CrudStatus.Deleted))
                    .Select(branch => branch.ToBranch()).ToList();

            var now = DateTime.UtcNow;
            _repository.CreateAll(deletedDbBranches.Select(branch => new DbDeletedBranch
            {
                DeletionTime = now,
                ClientId = client.Id,
                DeletorUserId = _user.Id,
                DeletedItem = branch
            }));

            var deletedAddress =
                client.Addresses.Where(dbAddress => request.Addresses.All(reqAddress => reqAddress.Id != dbAddress.Id));

            client.Addresses = request.Addresses;

            _repository.CreateAll(deletedAddress.Select(address => new DbDeletedAddress
            {
                DeletionTime    = now,
                ClientId = client.Id,
                DeletorUserId = _user.Id,
                DeletedItem = address
            }));

            client.Name = request.Name;
            client.TrucksPerDay = request.TrucksPerDay;

            _repository.Save(client);

            return Response.Success(client);
        }

        public DbClient GetClient(string clientId)
        {
            return GetAll().FirstOrDefault(client => client.Id == clientId);
        }
    }
}
