using System.Collections.Generic;
using Ssi.TrackTruck.Bussiness.DAL.Trips;

namespace Ssi.TrackTruck.Bussiness.Trips
{
    public class TripResponse
    {
        public DbTrip Trip { get; set; }
        public IEnumerable<DbTripDrop> Drops { get; set; }
    }
}
