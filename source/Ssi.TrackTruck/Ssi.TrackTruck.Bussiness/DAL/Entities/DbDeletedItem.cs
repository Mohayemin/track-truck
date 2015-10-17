using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Ssi.TrackTruck.Bussiness.DAL.Entities
{
    [BsonIgnoreExtraElements]
    public class DbDeletedItem<TItem> : IEntity
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        public TItem DeletedItem { get; set; }
        public DateTime DeletionTime { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]
        public string DeletorUserId { get; set; }

        #region TODO: Get rid of the items below
        [BsonIgnore]
        public bool IsDeleted { get; set; }

        [BsonIgnore]
        public DateTime CreationTime { get; set; }

        [BsonIgnore]
        public string CreatorId { get; set; }
        #endregion
    }
}
