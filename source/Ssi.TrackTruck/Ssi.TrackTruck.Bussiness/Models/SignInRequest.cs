using System.ComponentModel.DataAnnotations;

namespace Ssi.TrackTruck.Bussiness.Models
{
    public class SignInRequest
    {
        [Required(ErrorMessage = "Username is empty")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Password is empty")]
        public string Password { get; set; }
        public bool RememberMe { get; set; }
    }
}
