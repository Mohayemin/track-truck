using System;
using System.Collections.Generic;
using Ssi.TrackTruck.Bussiness.DAL.Clients;
using Ssi.TrackTruck.Bussiness.DAL.Entities;
using Ssi.TrackTruck.Bussiness.DAL.Trips;

namespace Ssi.TrackTruck.Bussiness.DAL
{
    public class CollectionMapper
    {
        private Dictionary<Type, string> _mapping;

        public CollectionMapper()
        {
            _mapping = new Dictionary<Type, string>
            {
                {typeof(Client), "clients"},
                {typeof(Employee), "employees"},
                {typeof(Truck), "trucks"},
                {typeof(User), "users"},
                {typeof(Wirehouse), "wirehouses"},
                {typeof(Trip), "trips"},
            };
        }

        public string Map(Type type)
        {
            return _mapping[type];
        }
    }
}
