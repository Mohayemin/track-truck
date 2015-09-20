using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Ssi.TrackTruck.Bussiness.DAL.Entities
{
    public class Truck : IEntity
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string RegistrationNumber { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]
        public string CurrentTripId { get; set; }

        [BsonExtraElements]
        public BsonDocument ExtraElements_ { get; set; }
    }
}
