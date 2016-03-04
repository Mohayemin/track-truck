using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Ssi.TrackTruck.Bussiness.DAL.Constants;

namespace Ssi.TrackTruck.Bussiness.DAL.Trips
{
    public class DbTripCost
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        public TripCostType CostType { get; set; }
        public double OriginalCostInPeso { get; set; }
        public double ActualCostInPeso { get; set; }
        [BsonIgnore]
        public double Adjustment { get; set; }

        public string Comment { get; set; }

        public DbTripCost()
        {
            Id = ObjectId.GenerateNewId().ToString();
        }
    }
}