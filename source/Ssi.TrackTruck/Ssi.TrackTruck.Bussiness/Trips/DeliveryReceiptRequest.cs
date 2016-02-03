using System.ComponentModel.DataAnnotations;
using Ssi.TrackTruck.Bussiness.DAL.Trips;

namespace Ssi.TrackTruck.Bussiness.Trips
{
    public class DeliveryReceiptRequest
    {
        [Required]
        public string DrNumber { get; set; }
        
        [Range(1, int.MaxValue, ErrorMessage = "Number of boxes must be at least one")]
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