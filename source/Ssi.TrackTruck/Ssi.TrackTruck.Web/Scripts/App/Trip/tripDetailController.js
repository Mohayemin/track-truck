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

        $scope.deleteItem = function (tripId) {
            return tripService.delete(tripId).then(function (response) {
                return response;
            });
        };
    }
]);