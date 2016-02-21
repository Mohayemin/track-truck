using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Ssi.TrackTruck.Bussiness.DAL.Trips
{
    [BsonIgnoreExtraElements]
    public class DbDeliveryReceipt 
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string DrNumber { get; set; }
        public int NumberOfBoxes { get; set; }
        public int RejectedNumberOfBoxes { get; set; }
        public string Comment { get; set; }

        public DbDeliveryReceipt()
        {
            Id = ObjectId.GenerateNewId().ToString();
        }
    }
}