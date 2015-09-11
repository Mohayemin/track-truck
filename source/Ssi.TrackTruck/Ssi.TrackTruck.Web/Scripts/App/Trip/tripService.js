﻿trackTruck.factory('tripService', [
    '$http',
    'url',
    tripService
]);

function tripService($http, url) {
    return {
        orderTrip: function (request) {
            return $http.post(url.resolve('Trip', 'Order'), request);
        }
    };
}