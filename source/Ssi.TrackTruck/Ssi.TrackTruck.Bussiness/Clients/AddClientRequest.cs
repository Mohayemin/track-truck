using System.Collections.Generic;
using System.Linq;

namespace Ssi.TrackTruck.Bussiness.Clients
{
    public class AddClientRequest
    {
        public string Name { get; set; }
        public int TrucksPerDay { get; set; }

        public IList<AddBranchRequest> Branches { get; set; }

        public AddClientRequest()
        {
            Branches = new List<AddBranchRequest>();
        }

        public bool Validate()
        {
            var valid = !string.IsNullOrWhiteSpace(Name)
                   && TrucksPerDay >= 0;

            if (valid)
            {
                valid = Branches.Count == 0 || Branches.All(branch => branch.Validate());
            }

            return valid;
        }
    }
}
