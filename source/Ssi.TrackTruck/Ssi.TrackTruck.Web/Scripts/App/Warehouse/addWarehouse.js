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
                'globalMessage',
                function ($scope,
                    warehouseService,
                    globalMessage) {
                    $scope.request = {};
                    $scope.add = function () {
                        globalMessage.info('adding warehouse');
                        warehouseService.add($scope.request).then(function () {
                            $location.url('warehouse/list');
                            globalMessage.success('warehouse added');
                        }).catch(function (message) {
                            globalMessage.error(message);
                        });
                    };
                }
            ]
        };

        return directive;
    }
]);