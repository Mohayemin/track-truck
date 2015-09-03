namespace Ssi.TrackTruck.Bussiness.DAL.Entities
{
    public class User
    {
        public string Id { get; set; }
        public string Username { get; set; }
        public string UsernameLowerCase { get; set; }
        public string PasswordHash { get; set; }
    }
}
