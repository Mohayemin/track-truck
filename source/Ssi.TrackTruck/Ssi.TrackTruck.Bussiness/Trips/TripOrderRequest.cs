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
            var dbTrip = new DbTrip
            {
                ClientId = ClientId,
                PickupAddressId = PickupAddressId,
                ExpectedPickupTimeUtc = ExpectedPickupTime.PhilippinesToUtc(),
                
                TripTicketNumber = TripTicketNumber,
                TruckId = TruckId,
                Status = TripStatus.New,
                Costs = new List<DbTripCost>
                {
                    new DbTripCost(TripCostType.Fuel, FuelCost),
                    new DbTripCost(TripCostType.Parking, ParkingCost),
                    new DbTripCost(TripCostType.Toll, TollCost),
                    new DbTripCost(TripCostType.Barge, BargeCost),
                    new DbTripCost(TripCostType.Bundle, BundleCost)
                }
            };

            return dbTrip;
        }


        public IEnumerable<DbTripContract> ToContracts(string tripId)
        {
            yield return new DbTripContract(tripId, DriverId, DriverSalary, DriverAllowance);

            HelperIds = HelperIds.Where(s => !string.IsNullOrWhiteSpace(s)).ToList();
            foreach (var helperId in HelperIds.Where(helperId => !string.IsNullOrWhiteSpace(helperId)))
            {
                yield return new DbTripContract(tripId, helperId, HelperSalary, HelperAllowance);
            }

            yield return new DbTripContract(tripId, SupervisorId, 0, 0);
        }
    }
}
