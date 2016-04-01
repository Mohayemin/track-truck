using System;
using System.Collections.Generic;
using System.Linq;
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

        public List<DbHistory> UpdateHistories { get; set; }

        public List<DbTripCost> Costs { get; set; }

        private DbTripCost _totalCost;

        [BsonIgnore]
        public DbTripCost TotalCost {
            get
            {
                if (_totalCost == null)
                {
                    double? actualCost = null;
                    if (Costs.Any(cost => cost.ActualCostInPeso != null))
                    {
                        actualCost = Costs.Sum(cost => cost.ActualCostInPeso);
                    }
                    _totalCost = new DbTripCost
                    {
                        ExpectedCostInPeso = Costs.Sum(cost => cost.ExpectedCostInPeso),
                        ActualCostInPeso = actualCost
                    };
                }
                return _totalCost;
            }
        }

        public DbTrip()
        {
            Costs = new List<DbTripCost>();
            UpdateHistories = new List<DbHistory>();
        }
    }
}
