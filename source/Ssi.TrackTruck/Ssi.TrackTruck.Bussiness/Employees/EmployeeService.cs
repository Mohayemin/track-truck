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
            return Response.Success(request);
        }
    }
}
