trackTruck.factory('tripService', [
    'repository',
    tripService
]);

function tripService(repository) {
    return {
        orderTrip: function (request) {
            var flatProperties = ['ExpectedPickupTime', 'ExpectedPickupTime',
                'DriverAllowance', 'DriverSalary', 'HelperAllowance', 'HelperSalary',
                'Drops', 'WarehouseId', 'DriverId', 'HelperId'];
            var idProeprties = ['Client'];

            var foramtterRequest = {
            };

            flatProperties.forEach(function (prop) {
                foramtterRequest[prop] = request[prop];
            });
            idProeprties.forEach(function (prop) {
                foramtterRequest[prop + "Id"] = request[prop].Id;
            });

            return repository.post('Trip', 'Order', foramtterRequest);
        }
    };
}