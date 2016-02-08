using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Ssi.TrackTruck.Bussiness.DAL.Entities;

namespace Ssi.TrackTruck.Bussiness.DAL.Clients
{
    public class DbDeletedAddress : DbDeletedItem<DbTextItem>
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string ClientId { get; set; }
    }
}
