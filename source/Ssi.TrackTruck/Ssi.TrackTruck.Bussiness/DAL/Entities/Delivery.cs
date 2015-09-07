using System;

namespace Ssi.TrackTruck.Bussiness.DAL.Entities
{
    public class Delivery
    {
        public string Id { get; set; }
        public string DriverName { get; set; }
        public string TruckNumber { get; set; }
        public DateTime DeliveryDueTime { get; set; }
        public int ItemsToDeliver { get; set; }
        public int? ItemsDeliveredWithoutDamage { get; set; }
        public string Status { get; set; }

        public string FromOutlet { get; set; }
        public string ToOutlet { get; set; }
    }
}
