tripModule.controller('tripTransactionController', [
    '$scope',
    'tripService',
    'collection',
    'globalMessage',
    function (
        $scope
        , tripService
        , collection
        , globalMessage
        ) {

        tripService.getMyActiveDrops().then(function (drops) {
            $scope.drops = drops;
        });

        $scope.totalRejected = function(drop) {
            return collection.sum(drop.DeliveryReceipts, 'RejectedNumberOfBoxes');
        };

        $scope.totalAccepted = function(drop) {
            return drop.TotalBoxes - $scope.totalRejected(drop);
        };

        $scope.receive = function (drop) {
            tripService.receiveDrop(drop).then(function() {
                globalMessage.success('drop received');
            }).catch(function(message) {
                globalMessage.error(message);
            });
        };
    }
]);