trackTruck.controller('tripController', [
    '$scope',
    'tripService',
    '$filter',
    'dateFormat',
    tripController
]);

function tripController($scope, tripService, $filter, dateFormat) {
    $scope.addRequest = {};
    $scope.addTrip = function () {
        $scope.addRequest.DeliveryDate = $filter('date')($scope.addRequest.DeliveryDate, dateFormat);
        tripService.addTrip($scope.addRequest).then(function () {

        }).catch(function () {

        });
    };
}