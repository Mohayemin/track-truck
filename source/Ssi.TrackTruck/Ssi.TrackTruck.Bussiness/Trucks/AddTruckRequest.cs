using Ssi.TrackTruck.Bussiness.DAL.Entities;
using Ssi.TrackTruck.Bussiness.Models;

namespace Ssi.TrackTruck.Bussiness.Trucks
{
    public class AddTruckRequest
    {
        public string RegistrationNumber { get; set; }
        public string DriverId { get; set; }
        public string HelperId { get; set; }

        public DbTruck ToTruck()
        {
            return new DbTruck
            {
                CurrentTripId = null,
                RegistrationNumber = RegistrationNumber,
                DriverId = DriverId,
                HelperId = HelperId
            };
        }

        public Response Validate()
        {
            if (string.IsNullOrWhiteSpace(RegistrationNumber))
            {
                return Response.ValidationError("Registration number must be specified");
            }
            return Response.Success();
        }
    }
}
