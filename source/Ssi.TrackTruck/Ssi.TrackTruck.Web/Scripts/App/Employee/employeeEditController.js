employeeModule.controller('employeeEditController', [
    '$scope',
    '$routeParams',
    'employeeService',
    'designation',
    '$location',
    function ($scope,
        $routeParams,
        employeeService,
        designation,
        $location) {

        $scope.designations = {
            values: [designation.supervisor, designation.driver, designation.helper],
            selected: ''
        };

        employeeService.get($routeParams['id']).then(function(employee) {
            $scope.request = JSON.parse(JSON.stringify(employee));
            $scope.designations.selected = $scope.request.Designation;
        });

        $scope.save = function() {
            employeeService.edit($scope.request).then(function(employee) {
                $location.url('employee/list');
            });
        }
    }
]);