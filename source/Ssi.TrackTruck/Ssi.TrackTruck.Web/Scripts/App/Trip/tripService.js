tripModule.factory('tripService', [
    'repository',
    'signedInUser',
    'buildIdMap',
    '$q',
    function tripService(
        repository
        , signedInUser
        , buildIdMap
        , $q
        ) {
        var _activeTrips = [];
        var _tripById = {};

        var _loadActivePromise;
        
        var service = {
            getAllActive: function () {
                if (!_loadActivePromise) {
                    _loadActivePromise = repository.get('Trip', 'Active').then(function (trips) {
                        _activeTrips.length = 0;
                        _activeTrips.push.apply(_activeTrips, trips);

                        _tripById = buildIdMap(_activeTrips);

                        return _activeTrips;
                    });
                }

                return _loadActivePromise;
            },
            orderTrip: function (request) {
                var foramtterRequest = angular.extend({}, request);

                ['Client', 'Truck'].forEach(function (prop) {
                    foramtterRequest[prop + "Id"] = request[prop].Id;
                    delete foramtterRequest[prop];
                });

                return repository.post('Trip', 'Order', foramtterRequest);
            },
            getMyActiveDrops: function () {
                return service.getAllActive().then(function() {
                    return repository.get('Trip', 'MyActiveDrops');
                });
            },
            receiveDrop: function (drop) {
                var formattedRequest = {
                    DropId: drop.Id,
                    DeliveryRejections: {}
                };

                drop.DeliveryReceipts.forEach(function(dr) {
                    formattedRequest.DeliveryRejections[dr.Id] = dr.RejectedNumberOfBoxes;
                });

                return repository.post('Trip', 'Receive', formattedRequest).then(function(response) {
                    if (response.IsError) {
                        return $q.reject(response.Message);
                    }
                    return response;
                });
            },
            getReport: function(filter) {
                return repository.post('Trip', 'Report', filter);
            }
        };

        return service;
    }
]);
