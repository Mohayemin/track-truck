using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Ssi.TrackTruck.Bussiness.DAL.Entities
{
    public class DbTextItem : IDeletable
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string Value { get; set; }
        public bool IsDeleted { get; set; }

        public DbTextItem()
        {
            Id = ObjectId.GenerateNewId().ToString();
        }
    }
}
