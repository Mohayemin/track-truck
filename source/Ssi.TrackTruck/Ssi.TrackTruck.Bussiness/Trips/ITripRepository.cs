using System;
using System.Collections.Generic;
using System.Linq;
using Ssi.TrackTruck.Bussiness.DAL.Trips;

namespace Ssi.TrackTruck.Bussiness.Trips
{
    public interface ITripRepository
    {
        IQueryable<DbTripDrop> GetUsersActiveDrops(string userId);
        DbTripDrop GetDrop(string dropId);
        IEnumerable<string> GetUserBranchIds(string userId);
        IQueryable<DbTrip> GetTripsInRange(DateTime from, DateTime to);
        IQueryable<DbTripDrop> GetDropsOfTrips(IEnumerable<string> tripIds);
    }
}
