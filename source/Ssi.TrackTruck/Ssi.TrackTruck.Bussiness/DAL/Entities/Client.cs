using System.Collections.Generic;

namespace Ssi.TrackTruck.Bussiness.DAL.Entities
{
    public class Client
    {
        public string ClientId { get; set; }
        public string ClientName { get; set; }
        public int TrucksPerDay { get; set; }
        public IList<Store> Stores { get; set; }
    }
}
