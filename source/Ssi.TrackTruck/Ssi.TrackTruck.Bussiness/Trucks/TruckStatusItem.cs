using System.Linq;
using Ssi.TrackTruck.Bussiness.DAL.Constants;
using Ssi.TrackTruck.Bussiness.DAL.Entities;
using Ssi.TrackTruck.Bussiness.DAL.Trips;

namespace Ssi.TrackTruck.Bussiness.Trucks
{
    public class TruckStatusItem
    {
        private readonly DbTruck _dbTruck;
        private readonly DbTrip _dbTrip;

        public TruckStatusItem(DbTruck dbTruck, DbTrip dbTrip)
        {
            _dbTruck = dbTruck;
            _dbTrip = dbTrip;
        }

        public string TruckNumber { get { return _dbTruck.RegistrationNumber; } }
        public string DriverId { get { return _dbTrip.DriverId; } }
        public string DriverName { get { return null; } }
        public int ItemsCarrying { get { return _dbTrip.Drops.SelectMany(drop => drop.DeliveryReceipts).Sum(dr => dr.NumberOfBoxes); } }
        public TripStatus Status { get { return _dbTrip.Status; } }
    }
}
