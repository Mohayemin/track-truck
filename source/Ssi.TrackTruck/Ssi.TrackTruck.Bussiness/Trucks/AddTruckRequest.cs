using Ssi.TrackTruck.Bussiness.DAL.Entities;

namespace Ssi.TrackTruck.Bussiness.Trucks
{
    public class AddTruckRequest
    {
        public string RegistrationNumber { get; set; }

        public Truck ToTruck()
        {
            return new Truck { CurrentTripId = null, RegistrationNumber = RegistrationNumber };
        }

        public bool Validate()
        {
            return !string.IsNullOrWhiteSpace(RegistrationNumber);
        }
    }
}
