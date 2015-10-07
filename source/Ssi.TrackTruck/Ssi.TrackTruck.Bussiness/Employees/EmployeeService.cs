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

        public Response Add(DbEmployee request)
        {
            if (IsEmployeeNameEmpty(request.FirstName) || IsEmployeeNameEmpty(request.LastName))
            {
                return Response.Error("Validation");
            }
            if (IsDesignationEmpty(request.Designation))
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

        public Response Delete(string id)
        {
            _repository.SoftDelete<DbEmployee>(id);
            return Response.Success(null, "Successfully deleted");
        }

        private bool IsEmployeeNameEmpty(string name)
        {
            return string.IsNullOrWhiteSpace(name);
        }

        private bool IsDuplicateEmployeeName(DbEmployee request)
        {
            var nameTaken = _repository.Exists<DbEmployee>(e => e.FirstName == request.FirstName);
            return nameTaken;
        }

        private bool IsDesignationEmpty(string designation)
        {
            return string.IsNullOrWhiteSpace(designation);
        }
    }
}
