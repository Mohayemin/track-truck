tripModule.factory('tripService', [
    'repository',
    'signedInUser',
    'buildIdMap',
    '_',
    'clientService',
    'employeeService',
    'truckService',
    'collection',
    'Trip',
    '$q',
    function tripService(
        repository
        , signedInUser
        , buildIdMap
        , _
        , clientService
        , employeeService
        , truckService
        , collection
        , Trip
        , $q
        ) {

        var service = {
            orderTrip: function (request) {
                var foramtterRequest = angular.extend({}, request);

                ['Client', 'Truck'].forEach(function (prop) {
                    foramtterRequest[prop + "Id"] = request[prop].Id;
                    delete foramtterRequest[prop];
                });

                return repository.post('Trip', 'Order', foramtterRequest).then(function (response) {
                    if (response.IsError) {
                        return $q.reject(response.Message);
                    }
                    return response;
                });
            },
            getActiveTrips: function () {
                return repository.get('Trip', 'GetActiveTrips');
            },
            receiveDrop: function (drop) {
                var formattedRequest = {
                    DropId: drop.Id,
                    DeliveryRejections: {}
                };

                drop.DeliveryReceipts.forEach(function (dr) {
                    formattedRequest.DeliveryRejections[dr.Id] = dr.RejectedNumberOfBoxes;
                });

                return repository.post('Trip', 'Receive', formattedRequest).then(function (response) {
                    if (response.IsError) {
                        return $q.reject(response.Message);
                    }
                    return response;
                });
            },
            getReport: function (filter) {
                return repository.post('Trip', 'Report', filter).then(function (report) {
                    var trips = report.Trips.map(function (dbTrip) {
                        var drops = _.where(report.Drops, { TripId: dbTrip.Id });
                        return new Trip(dbTrip, drops);
                    });

                    return trips;
                });
            },
            get: function (tripId) {
                return repository.get('Trip', 'Get', { id: tripId });
            }
        };

        return service;
    }
]);
