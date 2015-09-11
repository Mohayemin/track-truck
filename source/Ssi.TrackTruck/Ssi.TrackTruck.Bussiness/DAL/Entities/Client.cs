using System.Collections.Generic;

namespace Ssi.TrackTruck.Bussiness.DAL.Entities
{
    public class Client
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public int TrucksPerDay { get; set; }
        public IList<Branch> Branches { get; set; }
    }
}
