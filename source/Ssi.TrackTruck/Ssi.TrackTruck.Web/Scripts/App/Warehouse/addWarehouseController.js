warhouseModule.controller('addWarehouseController', [
    '$scope',
    'warehouseService',
    'globalMessage',
    '$location',
    function($scope,
        warehouseService,
        globalMessage,
        $location) {
        $scope.request = {};
        $scope.add = function() {
            globalMessage.info('adding warehouse', 0);
            warehouseService.add($scope.request).then(function() {
                $location.url('warehouse/list');
                globalMessage.success('warehouse added');
            }).catch(function(message) {
                globalMessage.error(message);
            });
        };
    }
]);