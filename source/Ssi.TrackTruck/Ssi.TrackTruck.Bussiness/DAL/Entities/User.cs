using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Ssi.TrackTruck.Bussiness.Auth;

namespace Ssi.TrackTruck.Bussiness.DAL.Entities
{
    public class User : IEntity
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string Username { get; set; }
        public string UsernameLowerCase { get; set; }
        public string PasswordHash { get; set; }
        [BsonRepresentation(BsonType.String)]
        public Role Role { get; set; }
    }
}
