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
        public List<DbBranch> DeletedBranches { get; set; }
        public List<DbTextItem> DeletedAddresses { get; set; }

        public DbClient()
        {
            DeletedBranches = new List<DbBranch>();
            DeletedAddresses = new List<DbTextItem>();
        }
    }
}
