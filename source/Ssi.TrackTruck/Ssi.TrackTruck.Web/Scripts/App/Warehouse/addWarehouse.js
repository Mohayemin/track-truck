warhouseModule.directive('addWarehouse', [
    'url',
    '$location',
    function (url,
        $location) {
        var directive = {
            templateUrl: url.template('Warehouse', 'addWarehouse'),
            controller: [
                '$scope',
                'warehouseService',
                function ($scope,
                    warehouseService) {
                    $scope.request = {};
                    $scope.add = function() {
                        warehouseService.add($scope.request).then(function () {
                            $location.url('warehouse/list');
                        }).catch(function (message) {
                            console.error(message);
                        });
                    };
                }
            ]
        };

        return directive;
    }
]);