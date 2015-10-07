clientModule.directive('warehouseList', [
    'url',
    function (url) {
        return {
            templateUrl: url.template('Warehouse', 'warehouseList'),
            scope: {},
            controller: [
                '$scope',
                'warehouseService',
                function ($scope,
                    warehouseService) {
                    $scope.url = url;

                    $scope.loadWarehouses = function (dontForce) {
                        warehouseService.getAll(!dontForce).then(function (clients) {
                            $scope.warehouses = clients;
                        });
                    };

                    $scope.loadWarehouses(true);
                }
            ]
        };
    }
]);