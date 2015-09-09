using System.Collections.Generic;
using System.Linq;
using Ssi.TrackTruck.Bussiness.DAL;
using Ssi.TrackTruck.Bussiness.DAL.Entities;

namespace Ssi.TrackTruck.Bussiness.Trucks
{
    public class TruckService
    {
        private readonly IRepository _repository;

        public TruckService(IRepository repository)
        {
            _repository = repository;
        }

        public IEnumerable<TruckStatusItem> GetCurrentStatus()
        {
            var deliveries =
                _repository.GetAll<Delivery>().Where(delivery => delivery.Status != DeliveryStatus.Delivered);

            return deliveries.Select(delivery => new TruckStatusItem
            {
                DriverName = delivery.DriverName,
                Status = delivery.Status,
                ItemsCarrying = delivery.ItemsToDeliver,
                ToOutlet = delivery.ToOutlet,
                FromOutlet = delivery.FromOutlet,
            });
        } 
    }
}
