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
                var usernameLower = request.Username.ToLower();
                var user = _repository.FindOne<User>(u => u.UsernameLowerCase == usernameLower);
                var valid = user != null && _hasher.Match(request.Password, user.PasswordHash);
                if (valid)
                {
                    return Response.Success();
                }

                return Response.Error("InvalidCredentials", "Username an password does not match");
            }
            return Response.Error("Validation", "Please enter both username and password");
        }
    }
}
