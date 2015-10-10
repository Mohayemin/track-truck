truckModule.controller('truckEditController', [
    '$scope',
    '$routeParams',
    'truckService',
    'employeeService',
    'designation',
    '$location',
    function ($scope,
        $routeParams,
        truckService,
        employeeService,
        designation,
        $location) {

        truckService.get($routeParams['id']).then(function (truck) {
            $scope.request = JSON.parse(JSON.stringify(truck));
        });

        employeeService.getAllByDesignation(designation.driver).then(function (drivers) {
            $scope.drivers = drivers;
            $scope.request.driver = _.find($scope.drivers, { Id: $scope.request.DriverId });
        });

        employeeService.getAllByDesignation(designation.helper).then(function (helpers) {
            $scope.helpers = helpers;
            $scope.request.helper = _.find($scope.helpers, { Id: $scope.request.HelperId });
        });

        $scope.save = function () {
            truckService.edit($scope.request).then(function () {
                $location.url('truck/list');
            });
        }
    }
]);