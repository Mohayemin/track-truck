using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Ssi.TrackTruck.Bussiness.DAL.Entities;

namespace Ssi.TrackTruck.Bussiness.DAL.Clients
{
    public class DbClient : IEntity, ISoftDeletable
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string Name { get; set; }
        public int TrucksPerDay { get; set; }
        public IEnumerable<DbBranch> Branches { get; set; }
        public bool IsDeleted { get; set; }
        public List<string> Address { get; set; }
    }
}
