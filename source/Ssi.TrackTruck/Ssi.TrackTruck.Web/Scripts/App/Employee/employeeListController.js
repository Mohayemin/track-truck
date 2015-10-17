employeeModule.controller('employeeListController', [
    '$scope',
    'employeeService',
    function($scope,
        employeeService) {

        $scope.loadEmployees = function() {
            employeeService.getAll().then(function(employees) {
                $scope.employees = employees;
            }).catch(function() {
                console.error('could not load employees');
            });
        };

        $scope.loadEmployees();

        $scope.deleteItem = employeeService.delete;
    }
]);
