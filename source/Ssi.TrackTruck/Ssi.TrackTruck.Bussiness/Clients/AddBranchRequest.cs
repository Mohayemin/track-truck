using System.Linq;
using Ssi.TrackTruck.Bussiness.DAL.Clients;
using Ssi.TrackTruck.Bussiness.DAL.Entities;

namespace Ssi.TrackTruck.Bussiness.Clients
{
    public class AddBranchRequest
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }

        public bool Validate()
        {
            var fields = new[] { Name, Address, Username, Password, ConfirmPassword };

            var valid = fields.All(s => !string.IsNullOrWhiteSpace(s));

            valid = valid && Password == ConfirmPassword;

            return valid;
        }

        public Branch ToBranch()
        {
            return new Branch { Name = Name, Address = Address };
        }

    }
}
