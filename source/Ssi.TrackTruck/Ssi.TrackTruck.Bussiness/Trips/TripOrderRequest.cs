using System.Collections.Generic;
using System.Linq;
using Ssi.TrackTruck.Bussiness.DAL.Constants;
using Ssi.TrackTruck.Bussiness.DAL.Trips;
using Ssi.TrackTruck.Bussiness.Helpers;

namespace Ssi.TrackTruck.Bussiness.Trips
{
    public class TripOrderRequest
    {
        public string TripTicketNumber { get; set; }
        public string ClientId { get; set; }
        public string PickupAddress { get; set; }
        public DateTimeModel ExpectedPickupTime { get; set; }
        public string DriverId { get; set; }
        public double DriverAllowance { get; set; }
        public double DriverSalary { get; set; }
        public List<string> HelperIds { get; set; }
        public double HelperAllowance { get; set; }
        public double HelperSalary { get; set; }
        public string TruckId { get; set; }
        public string SupervisorId { get; set; }
        public double TollCost { get; set; }
        public double ParkingCost { get; set; }
        public double FuelCost { get; set; }
        public double BargeCost { get; set; }
        public double BundleCost { get; set; }
        public IList<TripDropRequest> Drops { get; set; }

        public DbTrip ToTrip()
        {
            return new DbTrip
            {
                ClientId = ClientId,
                PickupAddress = PickupAddress,
                ExpectedPickupTime = ExpectedPickupTime.ToDateTime(DateTimeConstants.PhilippineOffset),
                DriverId = DriverId,
                DriverAllowanceInCentavos = (long)(DriverAllowance * 100),
                DriverSalaryInCentavos = (long)(DriverSalary * 100),
                HelperIds = HelperIds,
                HelperAllowanceInCentavos = (long)(HelperAllowance * 100),
                HelperSalaryInCentavos = (long)(HelperSalary * 100),
                SupervisorId = SupervisorId,
                FuelCostInCentavos = (long)(FuelCost * 100),
                ParkingCostInCenvatos = (long)(ParkingCost * 100),
                TollCostInCentavos = (long) (TollCost * 100),
                BargeCostInCentavos = (long)(BargeCost * 100),
                BundleCostInCentavos = (long)(BundleCost * 100),
                TripTicketNumber = TripTicketNumber,
                TruckId = TruckId,
                Drops = Drops.Select(request => request.ToDrop()).ToList(),
                Status = TripStatus.NotStarted
            };
        }

    }
}
