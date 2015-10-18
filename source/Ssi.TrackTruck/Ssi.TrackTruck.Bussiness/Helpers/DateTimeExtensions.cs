using System;
using Ssi.TrackTruck.Bussiness.DAL.Constants;

namespace Ssi.TrackTruck.Bussiness.Helpers
{
    public static class DateTimeExtensions
    {
        public static DateTime ToPhilppines(this DateTime dateTime)
        {
            return new DateTimeOffset(dateTime).ToOffset(DateTimeConstants.PhilippineOffset).DateTime;
        }

        public static int DateInt(this DateTime dateTime)
        {
            var dateString = dateTime.ToString("yyyyMMdd");
            return int.Parse(dateString);
        }
    }
}
