using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Ssi.TrackTruck.Bussiness.DAL.Clients
{
    public class Branch
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }

        public Branch()
        {
            Id = ObjectId.GenerateNewId().ToString();
        }
    }
}