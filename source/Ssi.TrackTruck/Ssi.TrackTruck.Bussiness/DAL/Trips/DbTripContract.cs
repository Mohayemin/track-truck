using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Ssi.TrackTruck.Bussiness.DAL.Trips
{
    public class DbTripContract
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string EmployeeId { get; set; }
        public double SalaryInPeso { get; set; }
        public double AllowanceInPeso { get; set; }

        public DbTripContract()
        {
        }

        public DbTripContract(string employeeId, double salaryInPeso, double allowanceInPeso)
        {
            EmployeeId = employeeId;
            SalaryInPeso = salaryInPeso;
            AllowanceInPeso = allowanceInPeso;
        }
    }
}
