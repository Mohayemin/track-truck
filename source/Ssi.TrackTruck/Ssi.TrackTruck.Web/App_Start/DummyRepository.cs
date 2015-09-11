using System;
using System.Collections;
using System.Collections.Generic;
using FizzWare.NBuilder;
using Ssi.TrackTruck.Bussiness.DAL;
using Ssi.TrackTruck.Bussiness.DAL.Entities;

namespace Ssi.TrackTruck.Web
{
    public class DummyRepository : ListStorageRepository
    {
        public DummyRepository(IDictionary<Type, IList> data) : base(data)
        {
            var random = new Random();
            var trips = Builder<Trip>.CreateListOfSize(100).Build();
            var trucks = Builder<Truck>.CreateListOfSize(10).Random(random.Next(5, 9)).Do(truck => truck.CurrentTripId = trips[random.Next(99)].Id).Build();

            var clients = new List<Client>
            {
                new Client
                {
                    Id = "C1",
                    Name = "Avon",
                    TrucksPerDay = 15,
                    Branches = new List<Branch>
                    {
                        new Branch{Id = "B1", Name = "Condon", Address = "Condon, Philippines"},
                        new Branch{Id = "B2", Name = "Quezon", Address = "Quezon, Philippines"}
                    }
                }
            };

            data[typeof(Trip)] = (IList)trips;
            data[typeof(Truck)] = (IList)trucks;
            data[typeof(Client)] = clients;
        }
    }
}