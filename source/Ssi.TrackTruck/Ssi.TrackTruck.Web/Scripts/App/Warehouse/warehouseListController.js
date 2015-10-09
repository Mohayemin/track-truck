clientModule.controller('warehouseListController', [
    '$scope',
    'warehouseService',
    'url',
    function ($scope,
        warehouseService,
        url) {
        $scope.url = url;

        $scope.loadWarehouses = function (dontForce) {
            warehouseService.getAll(!dontForce).then(function (clients) {
                $scope.warehouses = clients;
            });
        };

        $scope.loadWarehouses(true);
    }
]);