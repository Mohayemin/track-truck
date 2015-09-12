using System.Collections.Generic;
using Ssi.TrackTruck.Bussiness.Helpers;

namespace Ssi.TrackTruck.Bussiness.Trips
{
    public class TripDropRequest
    {
        public string BranchId { get; set; }
        public DateTimeModel ExpectedDropTime { get; set; }
        public IList<DeliveryReceiptRequest> DeliveryReceipts { get; set; }
    }
}