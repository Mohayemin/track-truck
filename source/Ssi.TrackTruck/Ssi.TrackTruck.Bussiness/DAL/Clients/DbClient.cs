using System.Collections.Generic;
using Ssi.TrackTruck.Bussiness.DAL.Entities;

namespace Ssi.TrackTruck.Bussiness.DAL.Clients
{
    public class DbClient : Entity
    {
        public string Name { get; set; }
        public int TrucksPerDay { get; set; }
        public IList<DbBranch> Branches { get; set; }
        public IList<DbTextItem> Addresses { get; set; }
    }
}
