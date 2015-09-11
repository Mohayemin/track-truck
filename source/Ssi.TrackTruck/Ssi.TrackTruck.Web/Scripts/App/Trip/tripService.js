trackTruck.factory('tripService', [
    'repository',
    tripService
]);

function tripService(repository) {
    return {
        orderTrip: function (request) {
            return repository.post('Trip', 'Order', request);
        }
    };
}