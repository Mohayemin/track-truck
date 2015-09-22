using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Ssi.TrackTruck.Bussiness.DAL.Entities;

namespace Ssi.TrackTruck.Bussiness.DAL.Trips
{
    public class DbDeliveryReceipt : IEntity
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string DrNumber { get; set; }
        public int NumberOfBoxes { get; set; }

        public DbDeliveryReceipt()
        {
            Id = ObjectId.GenerateNewId().ToString();
        }
    }
}