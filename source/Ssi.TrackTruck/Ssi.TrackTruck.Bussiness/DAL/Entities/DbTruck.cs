﻿using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Ssi.TrackTruck.Bussiness.DAL.Entities
{
    public class DbTruck : Entity
    {
        public string RegistrationNumber { get; set; }
        [BsonRepresentation(BsonType.ObjectId)]
        public string CurrentTripId { get; set; }
        [BsonRepresentation(BsonType.ObjectId)]
        public string DriverId { get; set; }
        [BsonRepresentation(BsonType.ObjectId)]
        public string HelperId { get; set; }
    }
}
