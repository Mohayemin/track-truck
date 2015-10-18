using System;
using Ssi.TrackTruck.Bussiness.DAL.Constants;

namespace Ssi.TrackTruck.Bussiness.Helpers
{
    public static class DateTimeExtensions
    {
        public static readonly TimeSpan PhilippineOffset = new TimeSpan(0, 8, 0, 0);

        public static DateTime ToPhilppines(this DateTime dateTime)
        {
            return new DateTimeOffset(dateTime).ToOffset(PhilippineOffset).DateTime;
        }

        public static DateTime PhilippinesToUtc(this DateTime dateTime)
        {
            var phil = new DateTimeOffset(dateTime, PhilippineOffset);
            return phil.ToUniversalTime().DateTime;
        }

        public static int DateInt(this DateTime dateTime)
        {
            var dateString = dateTime.ToString("yyyyMMdd");
            return int.Parse(dateString);
        }
    }
}
