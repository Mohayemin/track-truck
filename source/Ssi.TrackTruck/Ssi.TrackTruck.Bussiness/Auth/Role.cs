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
        Encoder = 4,
        Supervisor = 8
    }
}
