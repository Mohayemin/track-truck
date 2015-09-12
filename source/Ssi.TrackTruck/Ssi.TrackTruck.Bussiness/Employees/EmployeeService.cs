using System.Collections.Generic;
using Ssi.TrackTruck.Bussiness.DAL;
using Ssi.TrackTruck.Bussiness.DAL.Entities;

namespace Ssi.TrackTruck.Bussiness.Employees
{
    public class EmployeeService
    {
        private readonly IRepository _repository;

        public EmployeeService(IRepository repository)
        {
            _repository = repository;
        }

        public IEnumerable<Employee> GetAll()
        {
            return _repository.GetAll<Employee>();
        }
    }
}
