using Ssi.TrackTruck.Bussiness.DAL.Entities;

namespace Ssi.TrackTruck.Bussiness.DAL.Trips
{
    public class DbTripSalaryAdjustment : Entity
    {
        public string Comment { get; set; }
        public long AdjustmentInPeso { get; set; }
    }
}