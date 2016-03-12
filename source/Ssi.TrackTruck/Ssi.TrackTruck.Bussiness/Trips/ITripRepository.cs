using System;
using System.Collections.Generic;
using System.Linq;
using Ssi.TrackTruck.Bussiness.DAL.Trips;

namespace Ssi.TrackTruck.Bussiness.Trips
{
    public interface ITripRepository
    {
        DbTripDrop GetDrop(string dropId);
        IQueryable<DbTrip> GetTripsInRange(DateTime fromUtc, DateTime toUtc);
        IDictionary<string, IEnumerable<DbTripDrop>> GetIndexedDrops(IEnumerable<string> tripIds);
        IDictionary<string, IEnumerable<DbTripContract>> GetIndexedContracts(IEnumerable<string> tripIds);
    }
}
