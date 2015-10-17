using Ssi.TrackTruck.Bussiness.Helpers;

namespace Ssi.TrackTruck.Bussiness.Clients
{
    public class EditBranchRequest : AddBranchRequest
    {
        public string Id { get; set; }
        public CrudStatus ModificationStatus { get; set; }
    }
}
