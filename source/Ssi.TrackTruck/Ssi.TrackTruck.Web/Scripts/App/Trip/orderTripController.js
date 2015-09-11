trackTruck.controller('orderTripController', [
    '$scope',
    'clientService',
    'tripService',
    'wirehouseService',
    orderTripController
]);

function orderTripController($scope, clientService, tripService, wirehouseService) {
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

    $scope.getTotalBoxes = function(drop) {
        return drop.DeliveryReceipts.map(function(dr) {
            return dr.NumberOfBoxes;
        }).reduce(function (oldV, v) {
            return isNaN(v) ? oldV : (v + oldV);
        }, 0);
    };

    clientService.getAll().then(function(clients) {
        $scope.clients = clients;
    }).catch(function() {
        console.error('could not load clients');
    });

    wirehouseService.getAll().then(function(wirehouses) {
        $scope.wirehouses = wirehouses;
    }).catch(function() {
        console.error('could not load wirehouses');
    });

    $scope.order = function () {
        tripService.orderTrip($scope.request).then(function () {

        }).catch(function () {

        });
    };
}