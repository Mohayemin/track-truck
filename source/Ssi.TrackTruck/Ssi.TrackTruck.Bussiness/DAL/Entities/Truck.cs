using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Ssi.TrackTruck.Bussiness.DAL.Entities
{
    public class Truck
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string Number { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]
        public string CurrentTripId { get; set; }
    }
}
