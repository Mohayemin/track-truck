using System.Linq;

namespace Ssi.TrackTruck.Bussiness.Models
{
    public class CreateUserRequest
    {
        public string Username { get; set; }
        public string InitialPassword { get; set; }
        public string Role { get; set; }

        public bool Validate()
        {
            var nonNullables = new[] {Username, InitialPassword, Role};

            var hasNull = nonNullables.Any(string.IsNullOrEmpty);

            return !hasNull;
        }
    }
}
