using System;
using System.Collections;
using System.Collections.Generic;
using FizzWare.NBuilder;
using Ssi.TrackTruck.Bussiness.DAL;
using Ssi.TrackTruck.Bussiness.DAL.Entities;

namespace Ssi.TrackTruck.Web
{
    public class DummyRepository : ListStorageRepository
    {
        public DummyRepository(IDictionary<Type, IList> data) : base(data)
        {
            var random = new Random();
            var trips = Builder<Trip>.CreateListOfSize(100).Build();
            var trucks = Builder<Truck>.CreateListOfSize(10).Random(random.Next(5, 9)).Do(truck => truck.CurrentTripId = trips[random.Next(99)].Id).Build();

            data[typeof(Trip)] = (IList)trips;
            data[typeof(Truck)] = (IList)trucks;
        }
    }
}