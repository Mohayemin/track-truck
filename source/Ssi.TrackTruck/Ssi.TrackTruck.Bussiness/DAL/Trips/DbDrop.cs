using System;
using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Ssi.TrackTruck.Bussiness.DAL.Trips
{
    public class DbDrop
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]
        public string BranchId { get; set; }
        public DateTime ExpectedDropTime { get; set; }
        public IList<DbDeliveryReceipt> DeliveryReceipts { get; set; }
        public DbDrop()
        {
            Id = ObjectId.GenerateNewId().ToString();
        }

    }
}