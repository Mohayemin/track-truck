using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Ssi.TrackTruck.Bussiness.DAL.Entities;

namespace Ssi.TrackTruck.Bussiness.DAL.Trips
{
    public class DeliveryReceipt : IEntity
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string DrNumber { get; set; }
        public int NumberOfBoxes { get; set; }

        public DeliveryReceipt()
        {
            Id = ObjectId.GenerateNewId().ToString();
        }
    }
}