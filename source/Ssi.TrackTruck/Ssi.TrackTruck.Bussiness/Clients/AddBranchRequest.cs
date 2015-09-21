using System.Linq;
using Ssi.TrackTruck.Bussiness.DAL.Clients;

namespace Ssi.TrackTruck.Bussiness.Clients
{
    public class AddBranchRequest
    {
        public string Name { get; set; }
        public string Address { get; set; }

        public bool Validate()
        {
            var fields = new[] { Name, Address};

            var valid = fields.All(s => !string.IsNullOrWhiteSpace(s));

            return valid;
        }

        public Branch ToBranch()
        {
            return new Branch { Name = Name, Address = Address };
        }

    }
}
