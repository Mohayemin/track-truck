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
    'tripStatus',
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
        , tripStatus
        , $q
        ) {

        function setTripStatus(trip, statusIdOrObject) {
            var statusId, statusObject;
            if (statusIdOrObject.id) {
                statusId = statusIdOrObject.id;
                statusObject = statusIdOrObject;
            } else {
                statusId = statusIdOrObject;
                statusObject = tripStatus[statusIdOrObject];
            }
            trip.Status = statusId;
            trip.StatusObject = statusObject;
        }

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
                return repository.get('Trip', 'GetActiveTrips').then(function (tripResponses) {
                    return tripResponses.map(function (trip) {
                        return new Trip(trip);
                    });
                });
            },
            receiveDrop: function (drop, trip) {
                var formattedRequest = {
                    DropId: drop.Id,
                    DeliveryRejections: []
                };

                drop.DeliveryReceipts.forEach(function (dr) {
                    formattedRequest.DeliveryRejections.push({
                        DeliveryReceiptId: dr.Id,
                        RejectedNumberOfBoxes: dr.RejectedNumberOfBoxes,
                        Comment: dr.Comment
                    });
                });

                return repository.post('Trip', 'Receive', formattedRequest).then(function (response) {
                    if (response.IsError) {
                        return $q.reject(response.Message);
                    }
                    setTripStatus(trip, response.Data);
                    drop.IsDelivered = true;
                    return response;
                });
            },
            getReport: function (filter) {
                return repository.post('Trip', 'Report', filter).then(function (report) {
                    var trips = report.Trips.map(function (tripResponse) {
                        return new Trip(tripResponse);
                    });

                    return trips;
                });
            },
            get: function (tripId) {
                return repository.get('Trip', 'Get', { id: tripId }).then(function(response) {
                    return new Trip(response);
                });
            },
            saveAdjustment: function (trip, adjustments) {
                return repository.post('trip', 'SaveAdjustments', {
                    tripId: trip.Id,
                    Adjustments: adjustments
                });
            },
            archive: function(trip) {
                return repository.post('trip', 'updateStatus', {
                    tripId: trip.Id,
                    status: tripStatus.Archived.id
                }).then(function () {
                    setTripStatus(trip, tripStatus.Archived.id);
                    return trip;
                });
            }
        };

        return service;
    }
]);
