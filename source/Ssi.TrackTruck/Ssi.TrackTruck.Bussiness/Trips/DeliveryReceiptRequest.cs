using Ssi.TrackTruck.Bussiness.DAL.Trips;

namespace Ssi.TrackTruck.Bussiness.Trips
{
    public class DeliveryReceiptRequest
    {
        public string DrNumber { get; set; }
        public int NumberOfBoxes { get; set; }

        public DeliveryReceipt ToDeliveryReceipt()
        {
            return new DeliveryReceipt
            {
                DrNumber = DrNumber,
                NumberOfBoxes = NumberOfBoxes
            };
        }
    }
}