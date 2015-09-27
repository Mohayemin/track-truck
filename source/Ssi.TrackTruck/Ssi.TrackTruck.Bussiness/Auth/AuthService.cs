using System;
using System.Collections.Generic;
using System.Linq;
using Ssi.TrackTruck.Bussiness.Clients;
using Ssi.TrackTruck.Bussiness.DAL;
using Ssi.TrackTruck.Bussiness.DAL.Entities;
using Ssi.TrackTruck.Bussiness.DAL.Users;
using Ssi.TrackTruck.Bussiness.Models;

namespace Ssi.TrackTruck.Bussiness.Auth
{
    public class AuthService
    {
        private readonly IRepository _repository;
        private readonly ClientService _clientService;
        private readonly IHasher _hasher;

        public AuthService(IRepository repository, IHasher hasher, ClientService clientService)
        {
            _repository = repository;
            _hasher = hasher;
            _clientService = clientService;
        }

        public Response AuthenticateUser(SignInRequest request)
        {
            if (request.Validate())
            {
                var user = FindByUsername(request.Username);
                var valid = user != null && _hasher.Match(request.Password, user.PasswordHash);
                if (valid)
                {
                    return Response.Success(null, "Verified, redirecting...");
                }

                return Response.Error("InvalidCredentials", "Username and password does not match");
            }
            return Response.Error("Validation", "Please enter both username and password");
        }

        private DbUser FindByUsername(string username)
        {
            var usernameLower = username.ToLower();
            var user = _repository.FindOne<DbUser>(u => u.UsernameLowerCase == usernameLower);
            return user;
        }

        // TODO: refactor long method
        public Response CreateUser(AddUserRequest request)
        {
            var validation = request.Validate();
            if (validation.IsError)
            {
                return validation;
            }
            if (FindByUsername(request.Username) != null)
            {
                return Response.DuplicacyError("A user with this name is already registered");
            }

            var user = CreateUserObject(request);

            if (request.Role == Role.BranchCustodian)
            {
                var client = _clientService.GetClient(request.ClientId);

                if (client == null)
                {
                    return Response.ValidationError("The client you specified does not exist");
                }
                var branch = client.Branches.FirstOrDefault(dbBranch => dbBranch.Id == request.BranchId);
                if (branch == null)
                {
                    return Response.ValidationError("The branch you specified does not exist");
                }

                branch.CustodianUserId = user.Id;

                _repository.Save(client);
            }

            _repository.Create(user);

            return Response.Success(user, "User Added");
        }

        public DbUser CreateUserObject(AddUserRequest request)
        {
            var user = new DbUser
            {
                Username = request.Username,
                FirstName = request.FirstName,
                LastName = request.LastName,
                PasswordHash = _hasher.GenerateHash(request.InitialPassword),
                UsernameLowerCase = request.Username.ToLower(),
                Role = request.Role,
                DailyHitLog = new List<DateTime>()
            };
            return user;
        }

        public IEnumerable<DbUser> GetUserList()
        {
            return _repository.GetAllProjected<DbUser>(
                user => user.Id,
                user => user.FirstName,
                user => user.LastName,
                user => user.Role,
                user => user.Username
                );
        }

        public Response ChangePassword(ChangePasswordRequest request, string username)
        {
            var response = request.Validate();
            if (response.IsError)
            {
                return response;
            }

            var user = _repository.FindOne<DbUser>(u => u.UsernameLowerCase == username.ToLower());
            if (user == null)
            {
                return Response.ValidationError("User not found");
            }
            if (!IsValidCurrentPassword(request.CurrentPassword, user.PasswordHash))
            {
                return Response.ValidationError("Provided current password is invalid");
            }

            user.PasswordHash = _hasher.GenerateHash(request.NewPassword);
            _repository.Save(user);
            return Response.Success();
        }

        private bool IsValidCurrentPassword(string currentPassword, string dbPassword)
        {
            return _hasher.Match(currentPassword, dbPassword);
        }

        public bool UpdateDailyHit(string username, DateTime time)
        {
            var user = FindByUsername(username);
            if (user != null)
            {
                user.DailyHitLog.Add(time);
                _repository.Save(user);
                return true;
            }

            return false;
        }

        public DateTime? GetLastDailyHit(string username)
        {
            var user = FindByUsername(username);
            if (user == null)
            {
                throw new Exception("No user found: " + username);
            }

            if (user.DailyHitLog.Count == 0)
            {
                return null;
            }

            return user.DailyHitLog.OrderBy(time => time).Last();
        }
    }
}
