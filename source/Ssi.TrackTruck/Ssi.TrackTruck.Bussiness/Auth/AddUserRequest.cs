using System.Linq;
using Ssi.TrackTruck.Bussiness.Models;

namespace Ssi.TrackTruck.Bussiness.Auth
{
    public class AddUserRequest
    {
        public string Username { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string InitialPassword { get; set; }
        public Role Role { get; set; }
        public string ClientId { get; set; }
        public string BranchId { get; set; }
        public Response Validate()
        {
            if (string.IsNullOrWhiteSpace(Username))
            {
                return Response.ValidationError("Please enter a username");
            }
            if (string.IsNullOrWhiteSpace(FirstName))
            {
                return Response.ValidationError("Please enter first name");
            }
            if (string.IsNullOrWhiteSpace(LastName))
            {
                return Response.ValidationError("Please enter last name");
            }
            if (string.IsNullOrWhiteSpace(InitialPassword))
            {
                return Response.ValidationError("Please enter a password");
            }
            if (Role == 0)
            {
                return Response.ValidationError("Please choose a role");
            }
            if (Role == Role.BranchCustodian)
            {
                if (string.IsNullOrWhiteSpace(ClientId))
                {
                    return Response.ValidationError("Please choose a client");
                }
                if (string.IsNullOrWhiteSpace(BranchId))
                {
                    return Response.ValidationError("Please choose a branch");
                }
            }
            return Response.Success();
        }
    }
}
