﻿using System;
using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Ssi.TrackTruck.Bussiness.DAL.Constants;
using Ssi.TrackTruck.Bussiness.DAL.Entities;

namespace Ssi.TrackTruck.Bussiness.DAL.Trips
{
    public class Trip : IEntity
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public TripStatus Status { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]
        public string ClientId { get; set; }
        public DateTime ExpectedPickupTime { get; set; }
        
        [BsonRepresentation(BsonType.ObjectId)]
        public string DriverId { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]
        public string WirehouseId { get; set; }
        public long DriverAllowanceInCentavos { get; set; }
        public long DriverSalaryInCentavos { get; set; }
        
        [BsonRepresentation(BsonType.ObjectId)]
        public string HelperId { get; set; }
        public long HelperAllowanceInCentavos { get; set; }
        public long HelperSalaryInCentavos { get; set; }

        public IList<Drop> Drops { get; set; }
    }
}