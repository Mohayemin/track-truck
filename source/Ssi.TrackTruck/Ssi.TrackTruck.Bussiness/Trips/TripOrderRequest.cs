using System;
using System.Collections.Generic;
using Ssi.TrackTruck.Bussiness.DAL.Constants;
using Ssi.TrackTruck.Bussiness.DAL.Entities;
using Ssi.TrackTruck.Bussiness.DAL.Trips;
using Ssi.TrackTruck.Bussiness.Helpers;

namespace Ssi.TrackTruck.Bussiness.Trips
{
    /*var flatProperties = ['ExpectedPickupTime', 'ExpectedPickupTime',
                'DriverAllowance', 'DriverSalary', 'HelperAllowance', 'HelperSalary',
                'Drops', 'WirehouseId', 'DriverId', 'HelperId'];*/
    public class TripOrderRequest
    {
        public string ClientId { get; set; }
        public DateTimeModel ExpectedPickupTime { get; set; }
        public double DriverAllowance { get; set; }
        public string DriverId { get; set; }
        public double DriverSalary { get; set; }
        public string HelperId { get; set; }
        public double HelperAllowance { get; set; }
        public double HelperSalary { get; set; }
        public IList<TripDropRequest> Drops { get; set; }
        public string WirehouseId { get; set; }

        public Trip ToTrip()
        {
            return null;
        }
    }
}
