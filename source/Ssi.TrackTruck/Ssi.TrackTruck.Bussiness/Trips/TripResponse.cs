using System.Collections.Generic;
using Ssi.TrackTruck.Bussiness.DAL.Trips;

namespace Ssi.TrackTruck.Bussiness.Trips
{
    public class TripResponse
    {
        public DbTrip Trip { get; private set; }
        public IEnumerable<DbTripDrop> Drops { get; private set; }
        public IEnumerable<DbTripContract> Contracts { get; private set; }

        public TripResponse(DbTrip trip, IEnumerable<DbTripDrop> drops, IEnumerable<DbTripContract> contracts)
        {
            Trip = trip;
            Drops = drops;
            Contracts = contracts;
        }
    }
}
