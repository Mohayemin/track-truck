using System;
using Ssi.TrackTruck.Bussiness.DAL;
using Ssi.TrackTruck.Bussiness.DAL.Entities;
using Ssi.TrackTruck.Bussiness.Models;

namespace Ssi.TrackTruck.Bussiness.Auth
{
    public class AuthService
    {
        private readonly IRepository _repository;
        private readonly IHasher _hasher;

        public AuthService(IRepository repository, IHasher hasher)
        {
            _repository = repository;
            _hasher = hasher;
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

                return Response.Error("InvalidCredentials", "Username an password does not match");
            }
            return Response.Error("Validation", "Please enter both username and password");
        }

        private User FindByUsername(string username)
        {
            var usernameLower = username.ToLower();
            var user = _repository.FindOne<User>(u => u.UsernameLowerCase == usernameLower);
            return user;
        }

        public Response CreateUser(CreateUserRequest request)
        {
            if (request.Validate())
            {
                if (FindByUsername(request.Username) == null)
                {
                    var user = new User
                    {
                        Id = Guid.NewGuid().ToString().Replace("-", "").ToLower(),
                        Username = request.Username,
                        PasswordHash = _hasher.GenerateHash(request.InitialPassword),
                        UsernameLowerCase = request.Username.ToLower(),
                        Role = request.Role
                    };

                    _repository.Create(user);
                    return Response.Success(user, "User Added");
                }

                return Response.Error("DuplicateUsername", "This username is already taken");
            }

            return Response.Error("Validation", "Please fill up the required fields");
        }
    }
}
