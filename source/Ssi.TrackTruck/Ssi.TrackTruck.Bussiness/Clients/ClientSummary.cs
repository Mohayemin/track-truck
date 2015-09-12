using System.Linq;
using Ssi.TrackTruck.Bussiness.DAL.Clients;

namespace Ssi.TrackTruck.Bussiness.Clients
{
    public class ClientSummary
    {
        public string Name { get; set; }
        public int TrucksPerDay { get; set; }
        public int NumberOfBranches { get; set; }

        public ClientSummary(Client client)
        {
            Name = client.Name;
            TrucksPerDay = client.TrucksPerDay;
            NumberOfBranches = client.Branches.Count();
        }
    }
}
