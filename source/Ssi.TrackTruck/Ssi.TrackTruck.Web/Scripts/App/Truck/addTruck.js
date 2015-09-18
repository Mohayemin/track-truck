trackTruck.directive('addTruck', [
    'url',
    'truckService',
    addTruckDirective
]);

function addTruckDirective(url, truckService) {
    return {
        templateUrl: url.template('Truck', 'addTruck'),
        scope: {},
        controller: [
            '$scope',
            function ($scope) {
                $scope.model = {};
                $scope.add = function() {
                    truckService.add($scope.model);
                };
            }
        ]
    };
}