using Ssi.TrackTruck.Bussiness.Auth;

namespace Ssi.TrackTruck.Bussiness.Models
{
    public class UserListResponseItem
    {
        public string Username { get; set; }

        public Role Role { get; set; }
    }
}