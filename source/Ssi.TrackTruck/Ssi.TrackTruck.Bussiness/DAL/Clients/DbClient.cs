using System.Collections.Generic;
using Ssi.TrackTruck.Bussiness.DAL.Entities;

namespace Ssi.TrackTruck.Bussiness.DAL.Clients
{
    public class DbClient : Entity
    {
        public string Name { get; set; }
        public int TrucksPerDay { get; set; }
        public List<DbBranch> Branches { get; set; }
        public List<DbTextItem> Addresses { get; set; }
    }
}
