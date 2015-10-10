using System;
using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Ssi.TrackTruck.Bussiness.DAL.Entities;

namespace Ssi.TrackTruck.Bussiness.DAL.Trips
{
    [BsonIgnoreExtraElements]
    public class DbTripDrop : Entity
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string TripId { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]
        public string BranchId { get; set; }
        public DateTime ExpectedDropTime { get; set; }
        public IList<DbDeliveryReceipt> DeliveryReceipts { get; set; }
    }
}