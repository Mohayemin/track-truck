using System;

namespace Ssi.TrackTruck.Bussiness.Auth
{
    /*
     * Synch with userRole in client side
     */
    [Flags]
    public enum Role
    {
        Admin = 1,
        BranchCustodian = 2,
        Encoder = 4
    }
}
