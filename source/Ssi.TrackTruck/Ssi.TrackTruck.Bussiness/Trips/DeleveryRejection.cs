namespace Ssi.TrackTruck.Bussiness.Trips
{
    public class DeleveryRejection
    {
        public string DeliveryReceiptId { get; set; }
        public int RejectedNumberOfBoxes { get; set; }
        public string Comment { get; set; }
    }
}