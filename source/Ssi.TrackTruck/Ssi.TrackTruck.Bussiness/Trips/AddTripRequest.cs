using System;
using Ssi.TrackTruck.Bussiness.DAL.Constants;
using Ssi.TrackTruck.Bussiness.DAL.Entities;

namespace Ssi.TrackTruck.Bussiness.Trips
{
    public class AddTripRequest
    {
        public int NumberOfItems { get; set; }
        public string ClientId { get; set; }
        public string FromWareHouseId { get; set; }
        public string ToBranchId { get; set; }

        public DateTime DeliveryDate { get; set; }
        public int DeliveryHour { get; set; }
        public int DeliveryMinute { get; set; }

        public Trip ToTrip()
        {
            var deliveryDueTime = new DateTime(DeliveryDate.Year, DeliveryDate.Month, DeliveryDate.Day,
                DeliveryHour, DeliveryMinute, 0, DateTimeKind.Unspecified);

            return new Trip
            {
                DeliveryDueTime = deliveryDueTime,
                NumberOfItems = NumberOfItems,
                Status = TripStatus.NotStarted,
                ClientId = ClientId,
                FromWareHouseId = FromWareHouseId,
                ToBranchId = ToBranchId
            };
        }

    }
}
