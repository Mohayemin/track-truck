namespace Ssi.TrackTruck.Bussiness.Auth
{
    public interface ISignedInUser
    {
        string Id { get;}
        bool IsSignedIn { get; }
    }
}
