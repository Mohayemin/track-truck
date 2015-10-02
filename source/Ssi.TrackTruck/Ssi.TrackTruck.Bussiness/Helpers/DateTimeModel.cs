using System;

namespace Ssi.TrackTruck.Bussiness.Helpers
{
    public class DateTimeModel
    {
        public int Year { get; set; }
        public int Month { get; set; }
        public int Day { get; set; }
        public int Hour { get; set; }
        public int Minute { get; set; }

        /// <summary>
        /// </summary>
        /// <param name="utcOffset">Time offset from UTC</param>
        /// <returns></returns>
        public DateTime ToDateTime(TimeSpan utcOffset)
        {
            var dateTime = new DateTimeOffset(Year, Month, Day, Hour, Minute, 0, utcOffset);
            return dateTime.DateTime;
        }

        public int DateInt
        {
            get { return int.Parse(string.Format("{0}{1}{2}", Year, Month.ToString("D2"), Day.ToString("D2"))); }
        }

        public int TimeInt
        {
            get { return int.Parse(string.Format("{0}{1}00", Hour.ToString("D2"), Minute.ToString("D2"))); }
        }
    }
}
