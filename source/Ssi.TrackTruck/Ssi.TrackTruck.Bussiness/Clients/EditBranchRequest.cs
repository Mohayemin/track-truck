using Ssi.TrackTruck.Bussiness.DAL.Clients;
using Ssi.TrackTruck.Bussiness.Helpers;

namespace Ssi.TrackTruck.Bussiness.Clients
{
    public class EditBranchRequest : AddBranchRequest
    {
        public string Id { get; set; }
        public CrudStatus ModificationStatus { get; set; }

        public void Update(DbBranch dbBranch)
        {
            dbBranch.Name = Name;
            dbBranch.Address = Address;
            dbBranch.CustodianUserId = CustodianUserId;
        }
    }
}
