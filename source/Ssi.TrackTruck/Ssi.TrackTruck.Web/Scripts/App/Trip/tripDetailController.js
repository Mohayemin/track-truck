tripModule.controller('tripDetailController', [
    'tripService',
    '$routeParams',
    '$scope',
    function (
        tripService,
        $routeParams,
        $scope
        ) {

        tripService.get($routeParams['id']).then(function (trip) {
            $scope.trip = trip;
        });
    }
]);