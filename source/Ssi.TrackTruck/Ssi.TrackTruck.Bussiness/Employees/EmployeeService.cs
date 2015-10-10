using System.Collections.Generic;
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
    }
}
