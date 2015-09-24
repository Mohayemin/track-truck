using System.Collections.Generic;
using Ssi.TrackTruck.Bussiness.DAL;
using Ssi.TrackTruck.Bussiness.DAL.Entities;
using Ssi.TrackTruck.Bussiness.Models;

namespace Ssi.TrackTruck.Bussiness.Warehouses
{
    public class WarehouseService
    {
        private readonly IRepository _repository;

        public WarehouseService(IRepository repository)
        {
            _repository = repository;
        }

        public IEnumerable<DbWarehouse> GetAll()
        {
            return _repository.GetAll<DbWarehouse>();
        }

        public Response AddWarehouse(AddWarehouseRequest request)
        {
            var response = request.Validate();
            if (response.IsError)
            {
                return response;
            }
            var isDuplicate = _repository.Exists<DbWarehouse>(wh => wh.Name == request.Name);
            if (isDuplicate)
            {
                return Response.Error("", "Another warehouse with same name already exists");
            }
            
            var warehouse = new DbWarehouse
            {
                Name = request.Name,
                Address = request.Address
            };

            _repository.Create(warehouse);

            return Response.Success(warehouse);
        }
    }
}
