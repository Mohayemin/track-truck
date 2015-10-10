tripModule.factory('tripService', [
    'repository',
    'signedInUser',
    'buildIdMap',
    'collection',
    function tripService(
        repository
        , signedInUser
        , buildIdMap
        , collection
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
            }
        };

        return service;
    }
]);
