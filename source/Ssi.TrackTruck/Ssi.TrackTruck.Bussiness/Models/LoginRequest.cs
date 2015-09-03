namespace Ssi.TrackTruck.Bussiness.Models
{
    public class LoginRequest
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public bool RememberMe { get; set; }

        public bool Validate()
        {
            return !(string.IsNullOrEmpty(Username) || string.IsNullOrEmpty(Password));
        }
    }
}
