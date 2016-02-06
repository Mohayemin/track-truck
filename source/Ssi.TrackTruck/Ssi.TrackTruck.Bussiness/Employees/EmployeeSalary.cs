using Ssi.TrackTruck.Bussiness.DAL.Entities;

namespace Ssi.TrackTruck.Bussiness.Employees
{
    public class EmployeeSalary
    {
        public DbEmployee Employee { get; set; }
        public long TotalAllowance { get; set; }
        public long TotalSalary { get; set; }
        public long TotalPayable { get { return TotalAllowance + TotalSalary; } }
    }
}
