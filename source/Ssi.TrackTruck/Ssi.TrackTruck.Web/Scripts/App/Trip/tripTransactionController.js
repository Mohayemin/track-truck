tripModule.controller('tripTransactionController', [
    '$scope',
    'tripService',
    function (
        $scope
        , tripService
        ) {

        tripService.getMyTrips().then(function (trips) {
            $scope.trips = trips;
        });
    }
]);