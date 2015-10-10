employeeModule.controller('employeeEditController', [
    '$scope',
    '$routeParams',
    'employeeService',
    'designation',
    function ($scope,
        $routeParams,
        employeeService,
        designation) {

        $scope.designations = {
            values: [designation.supervisor, designation.driver, designation.helper],
            selected: ''
        };

        employeeService.get($routeParams['id']).then(function(employee) {
            $scope.request = employee;
            $scope.designations.selected = employee.Designation;
        });
    }
]);