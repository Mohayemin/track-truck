using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Ssi.TrackTruck.Bussiness.Trips
{
    public class DropReceiveRequest
    {
        [Required]
        public string DropId { get; set; }
        public IList<DeleveryRejection> DeliveryRejections { get; set; }
    }
}
