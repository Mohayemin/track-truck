using System;
using Ssi.TrackTruck.Bussiness.DAL.Constants;
using Ssi.TrackTruck.Bussiness.DAL.Entities;

namespace Ssi.TrackTruck.Bussiness.Trips
{
    public class AddTripRequest
    {
        public DateTime DeliveryDueTime { get; set; }
        public int NumberOfItems { get; set; }
        public string ClientId { get; set; }
        public string FromWareHouseId { get; set; }
        public string ToBranchId { get; set; }

        public Trip ToTrip()
        {
            return new Trip
            {
                DeliveryDueTime = DateTime.SpecifyKind(DeliveryDueTime, DateTimeKind.Unspecified),
                NumberOfItems = NumberOfItems,
                Status = TripStatus.NotStarted,
                ClientId = ClientId,
                FromWareHouseId = FromWareHouseId,
                ToBranchId = ToBranchId
            };
        }

    }
}
