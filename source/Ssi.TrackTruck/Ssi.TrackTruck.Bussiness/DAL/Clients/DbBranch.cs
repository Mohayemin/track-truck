using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Ssi.TrackTruck.Bussiness.DAL.Clients
{
    [BsonIgnoreExtraElements]
    public class DbBranch
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]
        public string CustodianUserId { get; set; }
    }
}