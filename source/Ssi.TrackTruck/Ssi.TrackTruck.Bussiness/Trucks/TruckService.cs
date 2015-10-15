using System.Collections.Generic;
using Ssi.TrackTruck.Bussiness.DAL;
using Ssi.TrackTruck.Bussiness.DAL.Entities;
using Ssi.TrackTruck.Bussiness.Models;

namespace Ssi.TrackTruck.Bussiness.Trucks
{
    public class TruckService
    {
        private readonly IRepository _repository;

        public TruckService(IRepository repository)
        {
            _repository = repository;
        }

        public Response Add(AddTruckRequest request)
        {
            if (_repository.Exists<DbTruck>(dbTruck => dbTruck.RegistrationNumber == request.RegistrationNumber))
            {
                return Response.DuplicacyError("A truck with this registration number already exists");
            }
            var truck = _repository.Create(request.ToTruck());
            return Response.Success(truck);
        }

        public IEnumerable<DbTruck> GetAll()
        {
            return _repository.GetAllUndeleted<DbTruck>();
        }

        public Response Delete(string id)
        {
            var truck = _repository.SoftDelete<DbTruck>(id);
            if (truck != null)
            {
                return Response.Success(null, "Successfully deleted");
            }
            return Response.Error("", string.Format("The truck you tried to delete does not exist"));
        }

        public Response Save(EditTruckRequest request)
        {
            var truck = _repository.GetById<DbTruck>(request.Id);
            if (truck == null)
            {
                return Response.Error("", string.Format("The truck does not exist"));
            }

            if (_repository.Exists<DbTruck>(dbTruck => dbTruck.RegistrationNumber == request.RegistrationNumber))
            {
                return Response.DuplicacyError("A truck with this registration number already exists");
            }

            truck.RegistrationNumber = request.RegistrationNumber;
            truck.DriverId = request.DriverId;
            truck.HelperId = request.HelperId;

            _repository.Save(truck);
            return Response.Success(truck, "Successfully edited");
        }
    }
}
