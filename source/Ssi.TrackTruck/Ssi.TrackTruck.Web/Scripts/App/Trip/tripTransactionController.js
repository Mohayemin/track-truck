tripModule.controller('tripTransactionController', [
    '$scope',
    'tripService',
    'collection',
    function (
        $scope
        , tripService
        , collection
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

        $scope.receive = function(drop) {
            console.log(drop);
        };
    }
]);