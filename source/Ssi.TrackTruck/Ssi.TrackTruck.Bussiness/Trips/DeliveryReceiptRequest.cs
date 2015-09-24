using Ssi.TrackTruck.Bussiness.DAL.Trips;

namespace Ssi.TrackTruck.Bussiness.Trips
{
    public class DeliveryReceiptRequest
    {
        public string DrNumber { get; set; }
        public int NumberOfBoxes { get; set; }

        public DbDeliveryReceipt ToDeliveryReceipt()
        {
            return new DbDeliveryReceipt
            {
                DrNumber = DrNumber,
                NumberOfBoxes = NumberOfBoxes
            };
        }
    }
}