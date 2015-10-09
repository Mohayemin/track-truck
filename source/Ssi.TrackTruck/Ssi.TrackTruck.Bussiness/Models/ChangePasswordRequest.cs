using System.Linq;

namespace Ssi.TrackTruck.Bussiness.Models
{
    public class ChangePasswordRequest
    {
        public string CurrentPassword { get; set; }
        public string NewPassword { get; set; }
        public string ConfirmNewPassword { get; set; }

        public Response Validate()
        {
            if (string.IsNullOrWhiteSpace(CurrentPassword))
            {
                return Response.ValidationError("Current password cannot be empty");
            }
            if (string.IsNullOrWhiteSpace(NewPassword))
            {
                return Response.ValidationError("New password cannot be empty");
            }
            if (string.IsNullOrWhiteSpace(ConfirmNewPassword))
            {
                return Response.ValidationError("Please retype the new password");
            }
            if (NewPassword != ConfirmNewPassword)
            {
                return Response.ValidationError("New password and confirmation of new password doesn't match");
            }

            return Response.Success();
        }
    }
}
