using System.Collections.Generic;
using Ssi.TrackTruck.Bussiness.DAL;
using Ssi.TrackTruck.Bussiness.DAL.Entities;
using Ssi.TrackTruck.Bussiness.Models;
using System;
using System.Linq;
using Ssi.TrackTruck.Bussiness.DAL.Constants;
using Ssi.TrackTruck.Bussiness.Trips;

namespace Ssi.TrackTruck.Bussiness.Employees
{
    public class EmployeeService
    {
        private readonly IRepository _repository;
        private readonly ITripRepository _tripRepository;

        public EmployeeService(IRepository repository, ITripRepository tripRepository)
        {
            _repository = repository;
            _tripRepository = tripRepository;
        }

        public IEnumerable<DbEmployee> GetAll()
        {
            return _repository.GetAllUndeleted<DbEmployee>();
        }

        public Response Add(AddEmployeeRequest request)
        {
            var employee = new DbEmployee
            {
                Designation = request.Designation,
                FirstName = request.FirstName,
                LastName = request.LastName
            };

            _repository.Create(employee);
            return Response.Success(employee);
        }

        public Response Delete(string id)
        {
            var employee = _repository.SoftDelete<DbEmployee>(id);
            if (employee != null)
            {
                return Response.Success(null, "Successfully deleted");
            }
            return Response.Error("", string.Format("The employee you tried to delete does not exist"));
        }

        public Response Save(EditEmployeeRequest request)
        {
            var employee = _repository.GetById<DbEmployee>(request.Id);
            if (employee == null)
            {
                return Response.Error("", string.Format("The employee does not exist"));
            }

            employee.Designation = request.Designation;
            employee.FirstName = request.FirstName;
            employee.LastName = request.LastName;

            _repository.Save(employee);
            return Response.Success(employee, "Successfully edited");
        }

        public EmployeeSalaryReportResponse GetSalaryReport(DateTime fromDate, DateTime toDate)
        {
            fromDate = fromDate.ToUniversalTime();
            toDate = toDate.ToUniversalTime().AddDays(1).AddTicks(-1);

            var drivers = _repository.GetWhere<DbEmployee>(employee => employee.Designation == EmployeDesignations.Driver);
            var helpers = _repository.GetWhere<DbEmployee>(employee => employee.Designation == EmployeDesignations.Helper);
            var trips = _tripRepository.GetTripsInRange(fromDate, toDate);

            var employeeSalaries = new List<EmployeeSalary>();

            foreach (var driver in drivers)
            {
                var tripsForDriver = trips.Where(trip => trip.DriverId == driver.Id).ToList();
                var adjustment = tripsForDriver.SelectMany(trip => trip.Adjustments).Sum(_ => _.AdjustmentInPeso);

                employeeSalaries.Add(new EmployeeSalary
                {
                    Employee = driver,
                    TotalAllowance = tripsForDriver.Sum(trip => trip.DriverAllowanceInPeso),
                    TotalSalary = tripsForDriver.Sum(trip => trip.DriverSalaryInPeso),
                    TotalAdjustment = adjustment
                });
            }

            foreach (var helper in helpers)
            {
                var tripsForHelper = trips.Where(trip => trip.HelperIds.Contains(helper.Id)).ToList();
                employeeSalaries.Add(new EmployeeSalary
                {
                    Employee = helper,
                    TotalAllowance = tripsForHelper.Sum(trip => trip.HelperAllowanceInPeso),
                    TotalSalary = tripsForHelper.Sum(trip => trip.HelperSalaryInPeso),
                    TotalAdjustment = 0
                });
            }

            return new EmployeeSalaryReportResponse
            {
                FromDate = fromDate,
                ToDate = toDate,
                EmployeeSalaries = employeeSalaries
            };
        }
    }
}
