using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Ssi.TrackTruck.Bussiness.DAL.Entities
{
    public class DbTextItem
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string Text { get; set; }

        public DbTextItem()
        {
            Id = ObjectId.GenerateNewId().ToString();
        }
    }
}
