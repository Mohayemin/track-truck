using System;
using System.Collections.Generic;
using System.Web.Script.Serialization;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Ssi.TrackTruck.Bussiness.Auth;
using Ssi.TrackTruck.Bussiness.DAL.Entities;

namespace Ssi.TrackTruck.Bussiness.DAL.Users
{
    public class DbUser : Entity
    {
        public string Username { get; set; }
        public string UsernameLowerCase { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [ScriptIgnore]
        public string PasswordHash { get; set; }
        [BsonRepresentation(BsonType.String)]
        public Role Role { get; set; }

        [BsonIgnore]
        public string FullName { get { return FirstName + " " + LastName; }}
    }
}
