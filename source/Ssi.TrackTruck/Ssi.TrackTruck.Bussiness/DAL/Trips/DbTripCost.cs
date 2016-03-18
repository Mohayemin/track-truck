using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Ssi.TrackTruck.Bussiness.DAL.Constants;

namespace Ssi.TrackTruck.Bussiness.DAL.Trips
{
    public class DbTripCost
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        [BsonRepresentation(BsonType.String)]
        public TripCostType CostType { get; set; }
        public double ExpectedCostInPeso { get; set; }
        public double? ActualCostInPeso { get; set; }
        [BsonIgnore]
        public double? AdjustmentInPeso {
            get
            {
                return ExpectedCostInPeso - ActualCostInPeso;
            }
        }

        public string Comment { get; set; }

        public DbTripCost()
        {
            Id = ObjectId.GenerateNewId().ToString();
        }

        public DbTripCost(TripCostType costType, double expectedCostInPeso, double? actualCostInPeso = null) : this()
        {
            CostType = costType;
            ExpectedCostInPeso = expectedCostInPeso;
            ActualCostInPeso = actualCostInPeso;
        }
    }
}