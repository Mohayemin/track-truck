using System.Collections.Generic;
using Ssi.TrackTruck.Bussiness.DAL;
using Ssi.TrackTruck.Bussiness.DAL.Entities;

namespace Ssi.TrackTruck.Bussiness.Warehouses
{
    public class WarehouseService
    {
        private readonly IRepository _repository;

        public WarehouseService(IRepository repository)
        {
            _repository = repository;
        }

        public IEnumerable<Warehouse> GetAll()
        {
            return _repository.GetAll<Warehouse>();
        }
    }
}
