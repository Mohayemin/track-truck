using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Ssi.TrackTruck.Bussiness.DAL.Entities;

namespace Ssi.TrackTruck.Bussiness.DAL.Trips
{
    public class DbTripContract : Entity
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string TripId { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]
        public string EmployeeId { get; set; }
        public double SalaryInPeso { get; set; }
        public double AllowanceInPeso { get; set; }

        public DbTripContract()
        {
        }

        public DbTripContract(string tripId, string employeeId, double salaryInPeso, double allowanceInPeso)
        {
            TripId = tripId;
            EmployeeId = employeeId;
            SalaryInPeso = salaryInPeso;
            AllowanceInPeso = allowanceInPeso;
        }
    }
}
