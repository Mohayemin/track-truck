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
        Drops: []
    };

    $scope.addDr = function (drop) {
        drop.DeliveryReceipts.push({});
    };

    $scope.addDrop = function () {
        var drop = {
            BranchId: null,
            ExpectedDropTime: {},
            DeliveryReceipts: []
        };
        $scope.addDr(drop);
        $scope.request.Drops.push(drop);
    };

    $scope.addDrop();


    $scope.order = function () {
        $scope.request.DeliveryDate = $filter('date')($scope.request.DeliveryDate, dateFormat);

        tripService.orderTrip($scope.request).then(function () {

        }).catch(function () {

        });
    };
}