using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Ssi.TrackTruck.Bussiness.DAL.Entities;

namespace Ssi.TrackTruck.Bussiness.DAL.Clients
{
    public class DbDeletedBranch : DbDeletedItem<DbBranch>
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string ClientId { get; set; }
    }
}
