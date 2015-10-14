using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Ssi.TrackTruck.Bussiness.DAL.Entities
{
    public abstract class Entity : IEntity
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public virtual bool IsDeleted { get; set; }
        public virtual DateTime CreationTime { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]
        public virtual string CreatorId { get; set; }

        [BsonExtraElements]
        public virtual BsonDocument UnMappedProperties { get; set; }

        protected Entity()
        {
            Id = ObjectId.GenerateNewId().ToString();
        }
    }
}