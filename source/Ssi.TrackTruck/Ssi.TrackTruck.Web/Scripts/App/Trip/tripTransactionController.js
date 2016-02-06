tripModule.controller('tripTransactionController', [
    '$scope',
    'tripService',
    'collection',
    'tripStatus',
    'globalMessage',
    function (
        $scope
        , tripService
        , collection
        , tripStatus
        , globalMessage
        ) {

        $scope.tripStatus = tripStatus;

        tripService.getActiveTrips().then(function (trips) {
            $scope.trips = trips;
        });

        $scope.totalRejected = function(drop) {
            return collection.sum(drop.DeliveryReceipts, 'RejectedNumberOfBoxes');
        };

        $scope.totalAccepted = function(drop) {
            return drop.TotalBoxes - $scope.totalRejected(drop);
        };

        $scope.receive = function (drop) {
            tripService.receiveDrop(drop).then(function () {
                drop.IsDelivered = true;
                globalMessage.success('drop received');
            }).catch(function(message) {
                globalMessage.error(message);
            });
        };
    }
]);