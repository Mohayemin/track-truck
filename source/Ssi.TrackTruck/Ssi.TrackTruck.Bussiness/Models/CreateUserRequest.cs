using System.Collections.Generic;
using System.Linq;
using Ssi.TrackTruck.Bussiness.Auth;

namespace Ssi.TrackTruck.Bussiness.Models
{
    public class CreateUserRequest
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
