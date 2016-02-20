using System;
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
        public string PickupAddressId { get; set; }
        public DateTime ExpectedPickupTime { get; set; }

        [Required(ErrorMessage = "Please choose a driver")]
        public string DriverId { get; set; }

        public double DriverAllowance { get; set; }
        public double DriverSalary { get; set; }
        public List<string> HelperIds { get; set; }
        public double HelperAllowance { get; set; }
        public double HelperSalary { get; set; }
        
        [Required(ErrorMessage = "Please choose a truck")]
        public string TruckId { get; set; }

        [Required(ErrorMessage = "Please choose released by")]
        public string SupervisorId { get; set; }
        public double TollCost { get; set; }
        public double ParkingCost { get; set; }
        public double FuelCost { get; set; }
        public double BargeCost { get; set; }
        public double BundleCost { get; set; }

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
                PickupAddressId = PickupAddressId,
                ExpectedPickupTimeUtc = ExpectedPickupTime.PhilippinesToUtc(),
                DriverId = DriverId,
                DriverAllowanceInPeso = DriverAllowance,
                DriverSalaryInPeso = DriverSalary,
                HelperIds = HelperIds.Where(id => !string.IsNullOrWhiteSpace(id)).ToList(),
                HelperAllowanceInPeso = HelperAllowance,
                HelperSalaryInPeso = HelperSalary,
                SupervisorId = SupervisorId,
                FuelCostInPeso = FuelCost,
                ParkingCostInPeso = ParkingCost,
                TollCostInPeso = TollCost,
                BargeCostInPeso = BargeCost,
                BundleCostInPeso = BundleCost,
                TripTicketNumber = TripTicketNumber,
                TruckId = TruckId,
                Status = TripStatus.New
            };
        }

    }
}
