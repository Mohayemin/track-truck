using Ssi.TrackTruck.Bussiness.DAL.Entities;

namespace Ssi.TrackTruck.Bussiness.Employees
{
    public class EmployeeSalary
    {

        public EmployeeSalary(DbEmployee employee, double salary, double allowance, double adjustment, int count)
        {
            Employee = employee;
            TotalSalary = salary;
            TotalAllowance = allowance;
            TotalAdjustment = adjustment;
            NumberOfTrips = count;
        }

        public DbEmployee Employee { get; set; }
        public double TotalAllowance { get; set; }
        public double TotalSalary { get; set; }
        public double TotalAdjustment { get; set; }
        public double TotalPayable { get { return TotalAllowance + TotalSalary + TotalAdjustment; } }
        public int NumberOfTrips { get; set; }
    }
}
