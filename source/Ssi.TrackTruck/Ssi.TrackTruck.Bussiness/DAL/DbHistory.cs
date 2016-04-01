using System;

namespace Ssi.TrackTruck.Bussiness.DAL
{
    public class DbHistory
    {
        public string UserId { get; set; }
        public DateTime TimeUtc { get; set; }
        public string Comment { get; set; }
        public object Data { get; set; }

        public DbHistory()
        {
        }

        public DbHistory(string userId, DateTime timeUtc, string comment, object data = null)
        {
            UserId = userId;
            TimeUtc = timeUtc;
            Comment = comment;
            Data = data;
        }
    }
}
