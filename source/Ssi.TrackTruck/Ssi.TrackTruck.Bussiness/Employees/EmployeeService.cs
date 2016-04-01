using System.Collections.Generic;
using Ssi.TrackTruck.Bussiness.DAL;
using Ssi.TrackTruck.Bussiness.DAL.Entities;
using Ssi.TrackTruck.Bussiness.Models;
using System;
using System.Linq;
using Ssi.TrackTruck.Bussiness.DAL.Constants;
using Ssi.TrackTruck.Bussiness.DAL.Trips;
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

        public IEnumerable<EmployeeSalary> GetSalaryReport(DateTime fromDate, DateTime toDate)
        {
            var trips = _tripRepository.GetTripsInRange(fromDate, toDate).Where(trip => trip.Status == TripStatus.Archived).ToList();

            var contracts = _repository.WhereIn<DbTripContract, string>(contract => contract.TripId,
                trips.Select(trip => trip.Id)).ToList();

            var employees = _repository.WhereIn<DbEmployee, string>(employee => employee.Designation, EmployeDesignations.DriverHelper);

            foreach (var employee in employees)
            {
                var employeeContracts = contracts.Where(contract => contract.EmployeeId == employee.Id).ToList();

                var employeeTripIds = employeeContracts.Select(contract => contract.TripId).Distinct();
                var employeeTrips = trips.Where(trip => employeeTripIds.Contains(trip.Id)).ToList();
                var employeeCosts = employeeTrips.SelectMany(trip => trip.Costs)
                    .Where(cost => cost.AssignedEmployeeId == employee.Id).ToList();

                var allowance = employeeContracts.Sum(contract => contract.AllowanceInPeso);
                var salary = employeeContracts.Sum(contract => contract.SalaryInPeso);
                
                var adjustment = employeeCosts
                    .Sum(cost => cost.AdjustmentInPeso ?? 0);

                yield return new EmployeeSalary(employee, salary, allowance, adjustment, employeeTrips.Count);
            }
        }
    }
}
