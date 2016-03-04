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
        [BsonRepresentation(BsonType.ObjectId)]
        public string DriverId { get; set; }
        [BsonRepresentation(BsonType.ObjectId)]
        public List<string> HelperIds { get; set; }
        [BsonRepresentation(BsonType.ObjectId)]
        public string SupervisorId { get; set; }

        public IList<DbTripCost> Costs { get; set; }

        public DbTrip()
        {
            Costs = new List<DbTripCost>();
        }
    }
}
