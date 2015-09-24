using System.Linq;

namespace Ssi.TrackTruck.Bussiness.Auth
{
    public class AddUserRequest
    {
        public string Username { get; set; }
        public string InitialPassword { get; set; }
        public Role Role { get; set; }

        public bool Validate()
        {
            var nonNullables = new[] { Username, InitialPassword };

            var hasNull = nonNullables.Any(string.IsNullOrEmpty);

            return !hasNull;
        }
    }
}
