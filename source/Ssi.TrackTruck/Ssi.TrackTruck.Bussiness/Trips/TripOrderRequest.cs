using System.Collections.Generic;
using System.Linq;
using Ssi.TrackTruck.Bussiness.DAL.Constants;
using Ssi.TrackTruck.Bussiness.DAL.Trips;
using Ssi.TrackTruck.Bussiness.Helpers;

namespace Ssi.TrackTruck.Bussiness.Trips
{
    public class TripOrderRequest
    {
        public string ClientId { get; set; }
        public string WirehouseId { get; set; }
        public DateTimeModel ExpectedPickupTime { get; set; }
        public string DriverId { get; set; }
        public double DriverAllowance { get; set; }
        public double DriverSalary { get; set; }
        public string HelperId { get; set; }
        public double HelperAllowance { get; set; }
        public double HelperSalary { get; set; }
        public IList<TripDropRequest> Drops { get; set; }

        public Trip ToTrip()
        {
            return new Trip
            {
                ClientId = ClientId,
                WirehouseId = WirehouseId,
                ExpectedPickupTime = ExpectedPickupTime.ToDateTime(DateTimeConstants.PhilippineOffset),
                DriverId = DriverId,
                DriverAllowanceInCentavos = (long)(DriverAllowance * 100),
                DriverSalaryInCentavos = (long)(DriverSalary * 100),
                HelperId = HelperId,
                HelperAllowanceInCentavos = (long)(HelperAllowance * 100),
                HelperSalaryInCentavos = (long)(HelperSalary * 100),
                Drops = Drops.Select(request => request.ToDrop()).ToList()
            };
        }
    }
}
