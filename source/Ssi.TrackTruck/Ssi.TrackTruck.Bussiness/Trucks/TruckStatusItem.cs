using Ssi.TrackTruck.Bussiness.DAL.Constants;
using Ssi.TrackTruck.Bussiness.DAL.Entities;

namespace Ssi.TrackTruck.Bussiness.Trucks
{
    public class TruckStatusItem
    {
        private readonly Truck _truck;
        private readonly Trip _trip;

        public TruckStatusItem(Truck truck, Trip trip)
        {
            _truck = truck;
            _trip = trip;
        }

        public string TruckNumber { get { return _truck.Number; }}
        public string DriverId { get { return _trip.DriverId; } }
        public string DriverName { get { return null; } }
        public int ItemsCarrying { get { return _trip.NumberOfItems; } }
        public TripStatus Status { get { return _trip.Status; } }
        public string FromOutlet { get { return _trip.FromOutletId; } }
        public string ToOutlet { get { return _trip.ToOutletId; } }
    }
}
