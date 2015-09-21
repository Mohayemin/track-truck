using System.Collections.Generic;
using System.Linq;
using Ssi.TrackTruck.Bussiness.DAL;
using Ssi.TrackTruck.Bussiness.DAL.Entities;
using Ssi.TrackTruck.Bussiness.Models;

namespace Ssi.TrackTruck.Bussiness.Employees
{
    public class EmployeeService
    {
        private readonly IRepository _repository;

        public EmployeeService(IRepository repository)
        {
            _repository = repository;
        }

        public IDictionary<string, List<Employee>> GetByDesignations(IEnumerable<string> designations)
        {
            return _repository.WhereIn<Employee, string>(employee => employee.Designation, designations)
                .GroupBy(employee => employee.Designation)
                .ToDictionary(group => group.Key, group => group.ToList());
        }

        public Response Add(Employee request)
        {
            if (IsEmployeeNameEmpty(request.Name))
            {
                return Response.Error("Validation");
            }
            if (IsDuplicateEmployeeName(request))
            {
                return Response.Error("", "Employee with same name already exists");
            }

            _repository.Create(request);
            return Response.Success(request);
        }

        private bool IsEmployeeNameEmpty(string name)
        {
            return string.IsNullOrWhiteSpace(name);
        }

        private bool IsDuplicateEmployeeName(Employee request)
        {
            var nameTaken = _repository.Exists<Employee>(e => e.Name == request.Name);
            return nameTaken;
        }
    }
}
