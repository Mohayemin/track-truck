using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using FizzWare.NBuilder;
using MongoDB.Bson;
using Ssi.TrackTruck.Bussiness.Auth;
using Ssi.TrackTruck.Bussiness.DAL;
using Ssi.TrackTruck.Bussiness.DAL.Clients;
using Ssi.TrackTruck.Bussiness.DAL.Constants;
using Ssi.TrackTruck.Bussiness.DAL.Entities;
using Ssi.TrackTruck.Bussiness.DAL.Users;

namespace Ssi.TrackTruck.Web.Controllers
{
    public class BackdoorController : ControllerBase
    {
        private readonly IRepository _repository;

        public BackdoorController(IRepository repository)
        {
            _repository = repository;
        }

        public ActionResult InsertData(string password)
        {
            if (password != "nooneknows")
            {
                return JsonNet("set password={password} param");
            }
            var admin = new DbUser
            {
                Id = ObjectId.Empty.ToString(),
                Username = "Admin",
                UsernameLowerCase = "admin",
                PasswordHash = "g+S4Aydl1ZTXWYxO8IdfJWVUJVCpeTc7D09FOEFfPT/rvjDhVFVe9pqfIFS8HfU36AMAAA==",
                Role = Role.Admin,
                FirstName = "Dummy",
                LastName = "Admin",
                IsDeleted = false,
                CreationTime = DateTime.MinValue,
                CreatorId = null
            };

            _repository.Save(admin);

            var client = Builder<DbClient>.CreateNew().Do(_ => _.Id = Oid).Build();
            client.Addresses = Builder<DbTextItem>.CreateListOfSize(2).All().Do(item => item.Id = Oid).Build().ToList();
            client.Branches = Builder<DbBranch>.CreateListOfSize(3).All().Do(branch => branch.Id = Oid).Build().ToList();
            client.CreatorId = admin.Id;
            _repository.Save(client);

            var driver = Builder<DbEmployee>.CreateNew().Do(_ => _.Id = Oid).Do(_ => _.CreatorId = admin.Id).Build();
            driver.Designation = EmployeDesignations.Driver;

            var helper = Builder<DbEmployee>.CreateNew().Do(_ => _.Id = Oid).Do(_ => _.CreatorId = admin.Id).Build();
            helper.Designation = EmployeDesignations.Helper;

            var supervisor = Builder<DbEmployee>.CreateNew().Do(_ => _.Id = Oid).Do(_ => _.CreatorId = admin.Id).Build();
            supervisor.Designation = EmployeDesignations.Supervisor;

            _repository.Save(driver);
            _repository.Save(helper);
            _repository.Save(supervisor);

            var truck = new DbTruck
            {
                CreatorId = admin.Id,
                IsDeleted = false,
                RegistrationNumber = "ABC-123"
            };

            _repository.Save(truck);

            var allItems = new List<object> { admin, client, driver, helper, supervisor, truck };
            return JsonNet(allItems);
        }

        public string Oid
        {
            get { return ObjectId.GenerateNewId().ToString(); }
        }
    }
}