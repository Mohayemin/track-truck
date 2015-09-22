using System.Collections.Generic;
using System.Linq;
using Ssi.TrackTruck.Bussiness.DAL.Constants;
using Ssi.TrackTruck.Bussiness.DAL.Trips;
using Ssi.TrackTruck.Bussiness.Helpers;

namespace Ssi.TrackTruck.Bussiness.Trips
{
    public class TripDropRequest
    {
        public string BranchId { get; set; }
        public DateTimeModel ExpectedDropTime { get; set; }
        public IList<DeliveryReceiptRequest> DeliveryReceipts { get; set; }


        public Drop ToDrop()
        {
            return new Drop
            {
                BranchId = BranchId,
                ExpectedDropTime = ExpectedDropTime.ToDateTime(DateTimeConstants.PhilippineOffset),
                DeliveryReceipts = DeliveryReceipts.Select(dr=>dr.ToDeliveryReceipt()).ToList()
            };
        }
    }
}