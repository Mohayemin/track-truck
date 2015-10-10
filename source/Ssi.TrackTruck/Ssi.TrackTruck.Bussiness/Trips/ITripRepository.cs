using System.Linq;
using Ssi.TrackTruck.Bussiness.DAL.Trips;

namespace Ssi.TrackTruck.Bussiness.Trips
{
    public interface ITripRepository
    {
        IQueryable<DbTripDrop> GetUsersActiveDrops(string userId);
    }
}
