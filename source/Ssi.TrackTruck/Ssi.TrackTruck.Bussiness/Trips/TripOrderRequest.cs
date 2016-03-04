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

        public List<string> HelperIds { get; set; }

        [Required(ErrorMessage = "Please choose a truck")]
        public string TruckId { get; set; }

        [Required(ErrorMessage = "Please choose released by")]
        public string SupervisorId { get; set; }

        public IList<DbTripCost> Costs { get; set; }

        public List<TripDropRequest> Drops { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (HelperIds == null || HelperIds.Count == 0 || HelperIds.TrueForAll(string.IsNullOrWhiteSpace))
            {
                yield return new ValidationResult("Please choose at least one helper");
            }

            if (Drops == null || Drops.Count < 1)
            {
                yield return new ValidationResult("Please setup at least one drop");
            }

            if (CheckCost(TripCostType.DriverAllowance, 1))
            {
                yield return new ValidationResult("Please set driver's allowance");
            }
            if (CheckCost(TripCostType.DriverSalary, 1))
            {
                yield return new ValidationResult("Please set driver's salary");
            }
            if (HelperIds != null)
            {
                if (CheckCost(TripCostType.HelperAllowance, HelperIds.Count))
                {
                    yield return
                        new ValidationResult(string.Format("Please set {0} helper's allowance", HelperIds.Count));
                }
                if (CheckCost(TripCostType.HelperAllowance, HelperIds.Count))
                {
                    yield return
                        new ValidationResult(string.Format("Please set {0} helper's salary", HelperIds.Count));
                }
            }
        }

        public bool CheckCost(TripCostType type, int count)
        {
            return Costs.Count(cost => cost.CostType == TripCostType.DriverAllowance) != count;
        } 

        public DbTrip ToTrip()
        {
            return new DbTrip
            {
                ClientId = ClientId,
                PickupAddressId = PickupAddressId,
                ExpectedPickupTimeUtc = ExpectedPickupTime.PhilippinesToUtc(),
                DriverId = DriverId,
                HelperIds = HelperIds.Where(id => !string.IsNullOrWhiteSpace(id)).ToList(),
                SupervisorId = SupervisorId,
                Costs = Costs,
                TripTicketNumber = TripTicketNumber,
                TruckId = TruckId,
                Status = TripStatus.New
            };
        }

    }
}
