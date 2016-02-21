using Ssi.TrackTruck.Bussiness.DAL.Entities;

namespace Ssi.TrackTruck.Bussiness.Employees
{
    public class EmployeeSalary
    {
        public DbEmployee Employee { get; set; }
        public double TotalAllowance { get; set; }
        public double TotalSalary { get; set; }
        public double TotalAdjustment { get; set; }
        public double TotalPayable { get { return TotalAllowance + TotalSalary + TotalAdjustment; } }
    }
}
