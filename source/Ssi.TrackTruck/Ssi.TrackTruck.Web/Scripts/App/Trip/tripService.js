tripModule.factory('tripService', [
    'repository',
    function tripService(repository) {
        return {
            orderTrip: function (request) {
                var foramtterRequest = angular.extend({}, request);

                ['Client', 'Truck'].forEach(function (prop) {
                    foramtterRequest[prop + "Id"] = request[prop].Id;
                    delete foramtterRequest[prop];
                });

                return repository.post('Trip', 'Order', foramtterRequest);
            },
            getReport: function(filter) {
                return repository.get('Trip', 'All');
            }
        };
    }
]);
