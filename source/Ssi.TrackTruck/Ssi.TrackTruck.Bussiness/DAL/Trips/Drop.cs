using System;
using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Ssi.TrackTruck.Bussiness.DAL.Trips
{
    public class Drop
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]
        public string BranchId { get; set; }
        public DateTime ExpectedDropTime { get; set; }
        public IList<DeliveryReceipt> DeliveryReceipts { get; set; }

        public Drop()
        {
            Id = ObjectId.GenerateNewId().ToString();
        }

    }
}