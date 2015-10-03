tripModule.factory('tripService', [
    'repository',
    function tripService(repository) {
        return {
            orderTrip: function(request) {
                var flatProperties = [
                    'ExpectedPickupTime', 'ExpectedPickupTime',
                    'DriverAllowance', 'DriverSalary', 'HelperAllowance', 'HelperSalary',
                    'Drops', 'PickupAddress', 'DriverId', 'HelperId'
                ];
                var idProeprties = ['Client'];

                var foramtterRequest = {
            
                };

                flatProperties.forEach(function(prop) {
                    foramtterRequest[prop] = request[prop];
                });
                idProeprties.forEach(function(prop) {
                    foramtterRequest[prop + "Id"] = request[prop].Id;
                });

                return repository.post('Trip', 'Order', foramtterRequest);
            }
        };
    }
]);
