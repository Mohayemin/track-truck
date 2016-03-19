using System;
using System.Collections.Generic;
using Ssi.TrackTruck.Bussiness.DAL.Trips;

namespace Ssi.TrackTruck.Bussiness.Trips
{
    public class TripReportResponse
    {
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public IEnumerable<TripResponse> Trips { get; set; }
        public IEnumerable<DbTripDrop> Drops { get; set; }
    }
}