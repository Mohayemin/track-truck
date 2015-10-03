using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Ssi.TrackTruck.Bussiness.DAL.Entities
{
    [BsonIgnoreExtraElements]
    public class DbEmployee : IEntity
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Designation { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreationTime { get; set; }

        [BsonIgnore]
        public string Name { get { return FirstName + " " + LastName; } }
    }
}
