using Ssi.TrackTruck.Bussiness.DAL;
using Ssi.TrackTruck.Bussiness.DAL.Entities;

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

        public bool AuthenticateUser(string username, string password)
        {
            var user = _repository.FindOne<User>(u => u.UsernameLowerCase == username);
            return user != null && _hasher.Match(password, user.PasswordHash);
        }
    }
}
