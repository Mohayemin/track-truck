using System.Collections.Generic;
using System.Linq;
using Ssi.TrackTruck.Bussiness.DAL.Clients;

namespace Ssi.TrackTruck.Bussiness.Clients
{
    public class AddClientRequest
    {
        public string Name { get; set; }
        public int TrucksPerDay { get; set; }

        public IList<AddBranchRequest> Branches { get; set; }

        public bool Validate()
        {
            var valid = !string.IsNullOrWhiteSpace(Name)
                   && TrucksPerDay >= 0;

            valid = valid && Branches.All(branch => branch.Validate());

            return valid;
        }
    }
}
