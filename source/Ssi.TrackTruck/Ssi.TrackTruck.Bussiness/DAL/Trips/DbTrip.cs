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
        public double FuelCostInPeso { get; set; }
        public double TollCostInPeso { get; set; }
        public double ParkingCostInPeso { get; set; }
        public double BargeCostInPeso { get; set; }
        public double BundleCostInPeso { get; set; }


        [BsonRepresentation(BsonType.ObjectId)]
        public string DriverId { get; set; }
        public double DriverAllowanceInPeso { get; set; }
        public double DriverSalaryInPeso { get; set; }


        [BsonRepresentation(BsonType.ObjectId)]
        public List<string> HelperIds { get; set; }
        public double HelperAllowanceInPeso { get; set; }
        public double HelperSalaryInPeso { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]
        public string SupervisorId { get; set; }

        public IList<DbTripSalaryAdjustment> Adjustments { get; set; }

        public DbTrip()
        {
            Adjustments = new List<DbTripSalaryAdjustment>();
        }
    }
}
