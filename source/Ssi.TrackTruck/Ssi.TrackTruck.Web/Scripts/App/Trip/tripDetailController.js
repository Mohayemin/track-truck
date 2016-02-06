tripModule.controller('tripDetailController', [
    'tripService',
    'Trip',
    '$routeParams',
    '$scope',
    function (
        tripService,
        Trip,
        $routeParams,
        $scope
        ) {

        tripService.get($routeParams['id']).then(function (trip) {
            $scope.trip = new Trip(trip.Trip, trip.Drops);
        });
    }
]);