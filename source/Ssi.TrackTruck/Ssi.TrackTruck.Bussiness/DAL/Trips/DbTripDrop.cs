﻿using System;
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
        public DateTime ExpectedDropTimeUtc { get; set; }
        public IList<DbDeliveryReceipt> DeliveryReceipts { get; set; }
        public bool IsDelivered { get; set; }
        public DateTime? ActualDropTimeUtc { get; set; }
        public string ReceiverUserId { get; set; }

        public DbTripDrop()
        {
            DeliveryReceipts = new List<DbDeliveryReceipt>();
        }

        [BsonIgnore]
        public int TotalBoxes
        {
            get { return DeliveryReceipts.Sum(receipt => receipt.NumberOfBoxes); }
        }

        [BsonIgnore]
        public int TotalRejectedBoxes
        {
            get { return IsDelivered ? DeliveryReceipts.Sum(receipt => receipt.RejectedNumberOfBoxes) : 0; }
        }

        public int TotalDeliveredBoxes
        {
            get { return IsDelivered ? TotalBoxes - TotalRejectedBoxes : 0; }
        }
    }
}