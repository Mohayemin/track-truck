using System;
using System.Collections.Generic;

namespace Ssi.TrackTruck.Bussiness.Employees
{
    public class EmployeeSalaryReportResponse
    {
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public IEnumerable<EmployeeSalary> EmployeeSalaries { get; set; }
    }
}
