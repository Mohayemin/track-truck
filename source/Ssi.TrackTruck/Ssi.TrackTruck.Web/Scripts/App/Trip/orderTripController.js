trackTruck.controller('orderTripController', [
    '$scope',
    'tripService',
    '$filter',
    'dateFormat',
    orderTripController
]);

function orderTripController($scope, tripService, $filter, dateFormat) {
    $scope.request = {
        DeliveryHour: 15,
        DeliveryMinute: 30,
        ExpectedPickupTime: {},
        Drops: [{}]
    };
    $scope.order = function () {
        $scope.request.DeliveryDate = $filter('date')($scope.request.DeliveryDate, dateFormat);

        tripService.orderTrip($scope.request).then(function () {

        }).catch(function () {

        });
    };
}