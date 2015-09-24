truckModule.directive('addTruck', [
    'url',
    'truckService',
    'employeeService',
    'designation',
    function addTruckDirective(
        url
        , truckService
        , employeeService
        , designation) {
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

                    employeeService.getAllByDesignation(designation.driver).then(function(drivers) {
                        $scope.drivers = drivers;
                    });

                    employeeService.getAllByDesignation(designation.helper).then(function (helpers) {
                        $scope.helpers = helpers;
                    });


                }
            ]
        };
    }
]);

