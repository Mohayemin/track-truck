trackTruck.factory('tripService', [
    '$http',
    'url',
    tripService
]);

function tripService($http, url) {
    return {
        addTrip: function (request) {
            return $http.post(url('Trip', 'Add'), request);
        }
    };
}