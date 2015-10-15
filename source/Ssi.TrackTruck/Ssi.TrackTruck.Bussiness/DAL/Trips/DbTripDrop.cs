using System;
using System.Collections.Generic;
using System.Linq;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Ssi.TrackTruck.Bussiness.DAL.Entities;

namespace Ssi.TrackTruck.Bussiness.DAL.Trips
{
    public class DbTripDrop : Entity
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string TripId { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]
        public string BranchId { get; set; }
        public DateTime ExpectedDropTime { get; set; }
        public IList<DbDeliveryReceipt> DeliveryReceipts { get; set; }

        public int TotalBoxes
        {
            get
            {
                return DeliveryReceipts != null ? DeliveryReceipts.Sum(receipt => receipt.NumberOfBoxes) : 0;
            }
        }

        public bool IsReceived { get; set; }
        public DateTime? ActualDropTime { get; set; }
        public string ReceiverUserId { get; set; }
    }
}