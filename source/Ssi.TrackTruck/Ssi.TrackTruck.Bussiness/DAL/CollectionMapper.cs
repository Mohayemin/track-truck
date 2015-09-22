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
                {typeof(DbClient), "clients"},
                {typeof(DbEmployee), "employees"},
                {typeof(DbTruck), "trucks"},
                {typeof(User), "users"},
                {typeof(DbWarehouse), "warehouses"},
                {typeof(DbTrip), "trips"},
            };
        }

        public string Map(Type type)
        {
            return _mapping[type];
        }
    }
}
