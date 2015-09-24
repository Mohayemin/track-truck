truckModule.directive('addTruck', [
    'globalMessage',
    '$location',
    'url',
    'truckService',
    'employeeService',
    'designation',
    function addTruckDirective(
        globalMessage
        , $location
        , url
        , truckService
        , employeeService
        , designation) {
        return {
            templateUrl: url.template('Truck', 'addTruck'),
            scope: {},
            controller: [
                '$scope',
                function ($scope) {
                    $scope.request = {};
                    $scope.add = function () {
                        globalMessage.info('adding truck', 0);
                        truckService.add($scope.request).then(function () {
                            globalMessage.success('truck added');
                            $location.url('truck/list');
                        }).catch(function (message) {
                            globalMessage.error(message);
                        });
                    };

                    employeeService.getAllByDesignation(designation.driver).then(function (drivers) {
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

