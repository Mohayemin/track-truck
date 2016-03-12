using System;
using System.Linq;
using System.Linq.Expressions;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using Ssi.TrackTruck.Bussiness.Auth;
using Ssi.TrackTruck.Bussiness.DAL.Entities;
using Ssi.TrackTruck.Bussiness.DAL.Trips;
using Ssi.TrackTruck.Bussiness.DAL.Users;

namespace Ssi.TrackTruck.Bussiness.DAL
{
    public class DbInitializer
    {
        private readonly MongoDatabase _db;
        private readonly CollectionMapper _mapper;

        public DbInitializer(MongoDatabase db, CollectionMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public void Init()
        {
            BuildIndexes();
            InsertDefaultData();
        }

        public void InsertDefaultData()
        {
            var admin = new DbUser
            {
                Id = ObjectId.Empty.ToString(),
                Username = "JR",
                UsernameLowerCase = "jr",
                PasswordHash = "g+S4Aydl1ZTXWYxO8IdfJWVUJVCpeTc7D09FOEFfPT/rvjDhVFVe9pqfIFS8HfU36AMAAA==",
                Role = Role.Admin,
                FirstName = "JRF",
                LastName = "Ibanez",
                IsDeleted = false,
                CreationTime = DateTime.MinValue,
                CreatorId = null
            };

            _db.GetCollection(_mapper.Map(typeof (DbUser))).Save(admin);
        }

        public void BuildIndexes()
        {
            BuildIndex<DbUser>(user => user.UsernameLowerCase);

            BuildIndex<DbDailyHit>(
                hit => hit.DatePhilippine,
                hit => hit.UserId);

            BuildIndex<DbTrip>(
                trip => trip.ClientId,
                trip => trip.DriverContract.EmployeeId,
                trip => trip.HelperContracts[0].EmployeeId,
                trip => trip.HelperContracts[1].EmployeeId,
                trip => trip.HelperContracts[2].EmployeeId,
                trip => trip.SupervisorContract.EmployeeId,
                trip => trip.Status);
        }

        private void BuildIndex<T>(params Expression<Func<T, object>>[] indexes)
        {
            foreach (var index in indexes)
            {
                var collectionName = _mapper.Map(typeof (T));
                _db.GetCollection<T>(collectionName).CreateIndex(IndexKeys<T>.Ascending(index));
            }
        }
    }
}
