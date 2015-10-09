using System;
using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Ssi.TrackTruck.Bussiness.DAL.Constants;
using Ssi.TrackTruck.Bussiness.DAL.Entities;

namespace Ssi.TrackTruck.Bussiness.DAL.Trips
{
    public class DbTrip : IEntity
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public TripStatus Status { get; set; }


        public string TripTicketNumber { get; set; }
        [BsonRepresentation(BsonType.ObjectId)]
        public string ClientId { get; set; }
        public string PickupAddress { get; set; }
        public DateTime ExpectedPickupTime { get; set; }
        public IEnumerable<DbDrop> Drops { get; set; }


        [BsonRepresentation(BsonType.ObjectId)]
        public string TruckId { get; set; }
        public long FuelCostInCentavos { get; set; }
        public long TollCostInCentavos { get; set; }
        public long ParkingCostInCenvatos { get; set; }
        public long BargeCostInCentavos { get; set; }
        public long BundleCostInCentavos { get; set; }

        
        [BsonRepresentation(BsonType.ObjectId)]
        public string DriverId { get; set; }
        public long DriverAllowanceInCentavos { get; set; }
        public long DriverSalaryInCentavos { get; set; }


        [BsonRepresentation(BsonType.ObjectId)]
        public List<string> HelperIds { get; set; }
        public long HelperAllowanceInCentavos { get; set; }
        public long HelperSalaryInCentavos { get; set; }
        
        
        [BsonRepresentation(BsonType.ObjectId)]
        public string SupervisorId { get; set; }
        
        public bool IsDeleted { get; set; }
        public DateTime CreationTime { get; set; }

        public DbTrip()
        {
            Drops = new List<DbDrop>();
        }
    }
}
