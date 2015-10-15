clientModule.controller('warehouseListController', [
    '$scope',
    'warehouseService',
    function ($scope,
        warehouseService
        ) {

        $scope.loadWarehouses = function (dontForce) {
            warehouseService.getAll(!dontForce).then(function (clients) {
                $scope.warehouses = clients;
            });
        };

        $scope.loadWarehouses(true);
    }
]);