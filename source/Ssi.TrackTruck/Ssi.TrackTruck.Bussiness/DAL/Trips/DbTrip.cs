using System;
using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Ssi.TrackTruck.Bussiness.DAL.Constants;
using Ssi.TrackTruck.Bussiness.DAL.Entities;

namespace Ssi.TrackTruck.Bussiness.DAL.Trips
{
    public class DbTrip : Entity
    {
        [JsonConverter(typeof(StringEnumConverter))]
        [BsonRepresentation(BsonType.String)]
        public TripStatus Status { get; set; }

        public string TripTicketNumber { get; set; }
        [BsonRepresentation(BsonType.ObjectId)]
        public string ClientId { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]
        public string PickupAddressId { get; set; }
        public DateTime ExpectedPickupTimeUtc { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]
        public string TruckId { get; set; }
        public long FuelCostInCentavos { get; set; }
        public long TollCostInCentavos { get; set; }
        public long ParkingCostInCentavos { get; set; }
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

        public IList<DbTripSalaryAdjustment> Adjustments { get; set; }

        public DbTrip()
        {
            Adjustments = new List<DbTripSalaryAdjustment>();
        }
    }
}
