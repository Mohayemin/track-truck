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
using Ssi.TrackTruck.Bussiness.DAL.Users;

namespace Ssi.TrackTruck.Web
{
    public class DummyRepository : ListStorageRepository
    {
        private static readonly IDictionary<Type, IList> Data;

        static DummyRepository()
        {
            Data = new Dictionary<Type, IList>();
            var random = new Random();
            var trips = Builder<DbTrip>.CreateListOfSize(5).Build();
            var trucks = Builder<DbTruck>.CreateListOfSize(10).Random(random.Next(5, 9)).Do(truck => truck.CurrentTripId = trips[random.Next(trips.Count)].Id).Build();
            var employees = Builder<DbEmployee>.CreateListOfSize(4).All().Do(e => e.Designation = EmployeDesignations.Driver).Build().ToList();
            employees.AddRange(Builder<DbEmployee>.CreateListOfSize(5).All().Do(e => e.Designation = EmployeDesignations.Helper).Build());

            var clients = new List<DbClient>
            {
                new DbClient
                {
                    Id = "C1",
                    Name = "Avon",
                    TrucksPerDay = 15,
                    Branches = new List<DbBranch>
                    {
                        new DbBranch{Id = "B1", Name = "Condon", Address = "Condon, Philippines"},
                        new DbBranch{Id = "B2", Name = "Quezon", Address = "Quezon, Philippines"}
                    }
                }
            };

            var users = new List<DbUser>
            {
                new DbUser
                {
                    Id = "1",
                    Username = "Mohayemin",
                    UsernameLowerCase = "mohayemin",
                    PasswordHash = "g+S4Aydl1ZTXWYxO8IdfJWVUJVCpeTc7D09FOEFfPT/rvjDhVFVe9pqfIFS8HfU36AMAAA=="
                },
                new DbUser
                {
                    Id = "2",
                    Username = "JR",
                    UsernameLowerCase = "jr",
                    PasswordHash = "g+S4Aydl1ZTXWYxO8IdfJWVUJVCpeTc7D09FOEFfPT/rvjDhVFVe9pqfIFS8HfU36AMAAA=="
                }
            };

            Data[typeof(DbTrip)] = (IList)trips;
            Data[typeof(DbTruck)] = (IList)trucks;
            Data[typeof(DbEmployee)] = employees;
            Data[typeof(DbClient)] = clients;
            Data[typeof (DbUser)] = users;
        }

        public DummyRepository() : base(Data)
        {
            
        }
    }
}