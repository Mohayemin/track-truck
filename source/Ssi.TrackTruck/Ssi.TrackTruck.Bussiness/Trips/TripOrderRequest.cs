using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Ssi.TrackTruck.Bussiness.DAL.Constants;
using Ssi.TrackTruck.Bussiness.DAL.Trips;
using Ssi.TrackTruck.Bussiness.Helpers;

namespace Ssi.TrackTruck.Bussiness.Trips
{
    public class TripOrderRequest : IValidatableObject
    {
        [Required(ErrorMessage = "Please enter trip ticket number")]
        public string TripTicketNumber { get; set; }

        [Required(ErrorMessage = "Please choose a client")]
        public string ClientId { get; set; }
        public string PickupAddress { get; set; }
        public DateTimeModel ExpectedPickupTime { get; set; }

        [Required(ErrorMessage = "Please choose a driver")]
        public string DriverId { get; set; }

        public double? DriverAllowance { get; set; }
        public double? DriverSalary { get; set; }
        public List<string> HelperIds { get; set; }
        public double? HelperAllowance { get; set; }
        public double? HelperSalary { get; set; }
        
        [Required(ErrorMessage = "Please choose a truck")]
        public string TruckId { get; set; }

        [Required(ErrorMessage = "Please choose released by")]
        public string SupervisorId { get; set; }
        public double? TollCost { get; set; }
        public double? ParkingCost { get; set; }
        public double? FuelCost { get; set; }
        public double? BargeCost { get; set; }
        public double? BundleCost { get; set; }

        public List<TripDropRequest> Drops { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (HelperIds == null || HelperIds.TrueForAll(string.IsNullOrWhiteSpace))
            {
                yield return new ValidationResult("Please choose at least one helper");
            }

            if (Drops == null || Drops.Count < 1)
            {
                yield return new ValidationResult("Please setup at least one drop");
            }
        }

        public DbTrip ToTrip()
        {
            return new DbTrip
            {
                ClientId = ClientId,
                PickupAddress = PickupAddress,
                ExpectedPickupTime = ExpectedPickupTime.ToDateTime(DateTimeConstants.PhilippineOffset),
                DriverId = DriverId,
                DriverAllowanceInCentavos = (long)(DriverAllowance ?? 0) * 100,
                DriverSalaryInCentavos = (long)(DriverSalary ?? 0) * 100,
                HelperIds = HelperIds.Where(id => !string.IsNullOrWhiteSpace(id)).ToList(),
                HelperAllowanceInCentavos = (long)(HelperAllowance ?? 0) * 100,
                HelperSalaryInCentavos = (long)(HelperSalary ?? 0) * 100,
                SupervisorId = SupervisorId,
                FuelCostInCentavos = (long)(FuelCost ?? 0) * 100,
                ParkingCostInCenvatos = (long)(ParkingCost ?? 0) * 100,
                TollCostInCentavos = (long)(TollCost ?? 0) * 100,
                BargeCostInCentavos = (long)(BargeCost ?? 0) * 100,
                BundleCostInCentavos = (long)(BundleCost ?? 0) * 100,
                TripTicketNumber = TripTicketNumber,
                TruckId = TruckId,
                Drops = Drops.Select(request => request.ToDrop()).ToList(),
                Status = TripStatus.NotStarted
            };
        }

    }
}
