using System.Collections.Generic;
using Ssi.TrackTruck.Bussiness.DAL;
using Ssi.TrackTruck.Bussiness.DAL.Entities;

namespace Ssi.TrackTruck.Bussiness.Wirehouses
{
    public class WirehouseService
    {
        private readonly IRepository _repository;

        public WirehouseService(IRepository repository)
        {
            _repository = repository;
        }

        public IEnumerable<Wirehouse> GetAll()
        {
            return _repository.GetAll<Wirehouse>();
        }
    }
}
