using Ssi.TrackTruck.Bussiness.DAL.Entities;

namespace Ssi.TrackTruck.Bussiness.Trucks
{
    public class AddTruckRequest
    {
        public string RegistrationNumber { get; set; }

        public DbTruck ToTruck()
        {
            return new DbTruck { CurrentTripId = null, RegistrationNumber = RegistrationNumber };
        }

        public bool Validate()
        {
            return !string.IsNullOrWhiteSpace(RegistrationNumber);
        }
    }
}
