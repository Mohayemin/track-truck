using System.Linq;
using Ssi.TrackTruck.Bussiness.DAL.Clients;

namespace Ssi.TrackTruck.Bussiness.Clients
{
    public class ClientSummary
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public int TrucksPerDay { get; set; }
        public int NumberOfBranches { get; set; }

        public ClientSummary(Client client)
        {
            Id = client.Id;
            Name = client.Name;
            TrucksPerDay = client.TrucksPerDay;
            NumberOfBranches = client.Branches.Count();
        }
    }
}
