using MongoDB.Bson.Serialization.Attributes;

namespace Ssi.TrackTruck.Bussiness.DAL.Entities
{
    public class DbEmployee : Entity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Designation { get; set; }

        [BsonIgnore]
        public string FullName { get { return FirstName + " " + LastName; } }
    }
}
