using MongoDB.Bson;
using Ssi.TrackTruck.Bussiness.DAL.Clients;
using Ssi.TrackTruck.Bussiness.Helpers;

namespace Ssi.TrackTruck.Bussiness.Clients
{
    public class EditBranchRequest : AddBranchRequest
    {
        public string Id { get; set; }
        public CrudStatus ModificationStatus { get; set; }

        public override DbBranch ToBranch()
        {
            var branch = base.ToBranch();
            branch.Id = ModificationStatus == CrudStatus.Added ? ObjectId.GenerateNewId().ToString() : Id;

            return branch;
        }
    }
}
