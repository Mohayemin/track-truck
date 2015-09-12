using System.Collections.Generic;
using Ssi.TrackTruck.Bussiness.DAL.Clients;

namespace Ssi.TrackTruck.Bussiness.Clients
{
    public class AddClientRequest
    {
        public string Name { get; set; }
        public int TrucksPerDay { get; set; }

        public bool Validate()
        {
            return !string.IsNullOrWhiteSpace(Name)
                   && TrucksPerDay >= 0;
        }

        public Client ToClient()
        {
            return new Client
            {
                Name = Name,
                TrucksPerDay = TrucksPerDay,
                Branches = new List<Branch>()
            };
        }
    }
}
