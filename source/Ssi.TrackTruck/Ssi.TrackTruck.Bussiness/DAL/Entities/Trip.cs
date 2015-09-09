using Ssi.TrackTruck.Bussiness.DAL.Constants;

namespace Ssi.TrackTruck.Bussiness.DAL.Entities
{
    public class Trip
    {
        public string Id { get; set; }
        public int NumberOfItems { get; set; }
        public string TruckId { get; set; }
        public TripStatus Status { get; set; }
        public string FromStoreId { get; set; }
        public string ToStoreId { get; set; }
        public string DriverId { get; set; }
        public string CargoLoaderId { get; set; }
        public int BillInCents { get; set; }
    }
}
