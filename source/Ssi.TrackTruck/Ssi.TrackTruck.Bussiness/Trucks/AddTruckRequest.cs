using Ssi.TrackTruck.Bussiness.DAL.Entities;

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

        public bool Validate()
        {
            return !string.IsNullOrWhiteSpace(RegistrationNumber);
        }
    }
}
