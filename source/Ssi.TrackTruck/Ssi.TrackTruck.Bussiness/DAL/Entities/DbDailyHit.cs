using System;
using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Ssi.TrackTruck.Bussiness.DAL.Entities
{
    public class DbDailyHit
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public DateTimeOffset Date { get; set; }
        public string UserId { get; set; }
        public List<TimeSpan> HitTimes { get; set; }
    }
}
