using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Ssi.TrackTruck.Bussiness.Trips
{
    public class DbTripContract
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string EmployeeId { get; set; }
        public double Salary { get; set; }
        public double Allowance { get; set; }

        public DbTripContract()
        {
        }

        public DbTripContract(string employeeId, double salary, double allowance)
        {
            EmployeeId = employeeId;
            Salary = salary;
            Allowance = allowance;
        }
    }
}
