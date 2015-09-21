using System;

namespace Ssi.TrackTruck.Bussiness.Auth
{
    [Flags]
    public enum Role
    {
        Admin = 1,
        BranchCustodian = 2
    }
}
