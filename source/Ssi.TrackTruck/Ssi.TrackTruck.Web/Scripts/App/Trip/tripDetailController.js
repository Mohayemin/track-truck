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

        tripService.get($routeParams['id']).then(function (tripResponse) {
            $scope.trip = new Trip(tripResponse);
        });
    }
]);