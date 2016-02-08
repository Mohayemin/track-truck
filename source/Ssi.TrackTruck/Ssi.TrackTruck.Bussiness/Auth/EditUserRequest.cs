using System.ComponentModel.DataAnnotations;

namespace Ssi.TrackTruck.Bussiness.Auth
{
    public class EditUserRequest : AddUserRequest
    {
        public string Id { get; set; }
    }
}
