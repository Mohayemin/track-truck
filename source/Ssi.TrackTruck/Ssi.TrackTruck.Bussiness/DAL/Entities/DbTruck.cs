using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Ssi.TrackTruck.Bussiness.DAL.Entities
{
    public class DbTruck : IEntity
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string RegistrationNumber { get; set; }
        [BsonRepresentation(BsonType.ObjectId)]
        public string CurrentTripId { get; set; }
        [BsonRepresentation(BsonType.ObjectId)]
        public string DriverId { get; set; }
        [BsonRepresentation(BsonType.ObjectId)]
        public string HelperId { get; set; }
        [BsonExtraElements]
        public BsonDocument ExtraElements_ { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreationTime { get; set; }
    }
}
