using System.Linq;
using Ssi.TrackTruck.Bussiness.DAL.Constants;
using Ssi.TrackTruck.Bussiness.DAL.Entities;
using Ssi.TrackTruck.Bussiness.DAL.Trips;

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

        public string TruckNumber { get { return _truck.RegistrationNumber; } }
        public string DriverId { get { return _trip.DriverId; } }
        public string DriverName { get { return null; } }
        public int ItemsCarrying { get { return _trip.Drops.SelectMany(drop => drop.DeliveryReceipts).Sum(dr => dr.NumberOfBoxes); } }
        public TripStatus Status { get { return _trip.Status; } }
    }
}
