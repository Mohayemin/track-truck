tripModule.controller('tripTransactionController', [
    '$scope',
    'tripService',
    function (
        $scope
        , tripService
        ) {

        tripService.getMyActiveDrops().then(function (drops) {
            $scope.drops = drops;
        });
    }
]);