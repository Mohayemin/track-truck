namespace Ssi.TrackTruck.Bussiness.Trucks
{
    public class TruckStatusItem
    {
        public string DriverId { get; set; }
        public string DriverName { get; set; }
        public int ItemsCarrying { get; set; }
        public string Status { get; set; }
        public string FromOutlet { get; set; }
        public string ToOutlet { get; set; }
    }
}
