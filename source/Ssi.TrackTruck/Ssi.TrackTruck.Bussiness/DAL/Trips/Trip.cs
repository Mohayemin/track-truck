using System;
using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Ssi.TrackTruck.Bussiness.DAL.Constants;

namespace Ssi.TrackTruck.Bussiness.DAL.Trips
{
    public class Trip
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public TripStatus Status { get; set; }
        public string ClientId { get; set; }
        public DateTime ExpectedPickupTime { get; set; }
        public string DriverId { get; set; }
        public string WirehouseId { get; set; }
        public long DriverAllowanceInCentavos { get; set; }
        public long DriverSalaryInCentavos { get; set; }
        public string HelperId { get; set; }
        public long HelperAllowanceInCentavos { get; set; }
        public long HelperSalaryInCentavos { get; set; }

        public IList<Drop> Drops { get; set; }
    }
}
