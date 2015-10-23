using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Ssi.TrackTruck.Bussiness.DAL.Trips;
using Ssi.TrackTruck.Bussiness.Helpers;

namespace Ssi.TrackTruck.Bussiness.Trips
{
    public class TripDropRequest : IValidatableObject
    {
        [Required(ErrorMessage = "Please choose branch")]
        public string BranchId { get; set; }
        public DateTime ExpectedDropTime { get; set; }
        public IList<DeliveryReceiptRequest> DeliveryReceipts { get; set; }

        public DbTripDrop ToDrop(string tripId)
        {
            return new DbTripDrop
            {
                TripId = tripId,
                BranchId = BranchId,
                ExpectedDropTimeUtc = ExpectedDropTime.PhilippinesToUtc(),
                DeliveryReceipts = DeliveryReceipts.Select(dr => dr.ToDeliveryReceipt()).ToList(),
            };
        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (ExpectedDropTime == default(DateTime))
            {
                yield return new ValidationResult("Please choose expected drop time");
            }
        }
    }
}