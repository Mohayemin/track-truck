using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using FizzWare.NBuilder;
using Ssi.TrackTruck.Bussiness.DAL;
using Ssi.TrackTruck.Bussiness.DAL.Clients;
using Ssi.TrackTruck.Bussiness.DAL.Constants;
using Ssi.TrackTruck.Bussiness.DAL.Entities;
using Ssi.TrackTruck.Bussiness.DAL.Trips;

namespace Ssi.TrackTruck.Web
{
    public class DummyRepository : ListStorageRepository
    {
        private static readonly IDictionary<Type, IList> Data;

        static DummyRepository()
        {
            Data = new Dictionary<Type, IList>();
            var random = new Random();
            var trips = Builder<Trip>.CreateListOfSize(5).Build();
            var trucks = Builder<Truck>.CreateListOfSize(10).Random(random.Next(5, 9)).Do(truck => truck.CurrentTripId = trips[random.Next(trips.Count)].Id).Build();
            var warehouses = Builder<Warehouse>.CreateListOfSize(8).Build();
            var employees = Builder<Employee>.CreateListOfSize(4).All().Do(e => e.Designation = EmployeDesignations.Driver).Build().ToList();
            employees.AddRange(Builder<Employee>.CreateListOfSize(5).All().Do(e => e.Designation = EmployeDesignations.Helper).Build());

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

            var users = new List<User>
            {
                new User
                {
                    Id = "1",
                    Username = "Mohayemin",
                    UsernameLowerCase = "mohayemin",
                    PasswordHash = "g+S4Aydl1ZTXWYxO8IdfJWVUJVCpeTc7D09FOEFfPT/rvjDhVFVe9pqfIFS8HfU36AMAAA=="
                },
                new User
                {
                    Id = "2",
                    Username = "JR",
                    UsernameLowerCase = "jr",
                    PasswordHash = "g+S4Aydl1ZTXWYxO8IdfJWVUJVCpeTc7D09FOEFfPT/rvjDhVFVe9pqfIFS8HfU36AMAAA=="
                }
            };

            Data[typeof(Trip)] = (IList)trips;
            Data[typeof(Truck)] = (IList)trucks;
            Data[typeof(Warehouse)] = (IList)warehouses;
            Data[typeof(Employee)] = employees;
            Data[typeof(Client)] = clients;
            Data[typeof (User)] = users;
        }

        public DummyRepository() : base(Data)
        {
            
        }
    }
}