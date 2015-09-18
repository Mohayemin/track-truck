truckModule.directive('addTruck', [
    'url',
    'truckService',
    function addTruckDirective(url, truckService) {
        return {
            templateUrl: url.template('Truck', 'addTruck'),
            scope: {},
            controller: [
                '$scope',
                function ($scope) {
                    $scope.model = {};
                    $scope.add = function () {
                        truckService.add($scope.model);
                    };
                }
            ]
        };
    }
]);

