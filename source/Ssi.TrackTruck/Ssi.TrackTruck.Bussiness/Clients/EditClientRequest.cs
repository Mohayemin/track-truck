using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Ssi.TrackTruck.Bussiness.DAL.Entities;

namespace Ssi.TrackTruck.Bussiness.Clients
{
    public class EditClientRequest
    {
        public string Id { get; set; }
        [Required(ErrorMessage = "Please specify client's name")]
        public string Name { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Truck per day cannot be less than zero")]
        public int TrucksPerDay { get; set; }

        public IList<DbTextItem> Addresses { get; set; }

    }
}
