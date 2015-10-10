tripModule.controller('addTruckController', [
    '$scope',
    'globalMessage',
    '$location',
    'truckService',
    'employeeService',
    'designation',
    function($scope, globalMessage, $location, truckService, employeeService, designation) {
        $scope.request = {};
        $scope.add = function() {
            globalMessage.info('adding truck', 0);
            truckService.add($scope.request).then(function() {
                globalMessage.success('truck added');
                $location.url('truck/list');
            }).catch(function(message) {
                globalMessage.error(message);
            });
        };

        employeeService.getAllByDesignation(designation.driver).then(function(drivers) {
            $scope.drivers = drivers;
        });

        employeeService.getAllByDesignation(designation.helper).then(function(helpers) {
            $scope.helpers = helpers;
        });
    }
]);

