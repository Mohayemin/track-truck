namespace Ssi.TrackTruck.Bussiness.Auth
{
    public interface IHasher
    {
        string GenerateHash(string plainText);
        bool Match(string plainText, string hashToMatchWith);
    }
}
