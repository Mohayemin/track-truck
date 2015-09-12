using System;
using System.Collections.Generic;

namespace Ssi.TrackTruck.Bussiness.DAL.Trips
{
    public class Drop
    {
        public string BranchId { get; set; }
        public DateTime ExpectedDropTime { get; set; }
        public IList<DeliveryReceipt> DeliveryReceipts { get; set; }
    }
}