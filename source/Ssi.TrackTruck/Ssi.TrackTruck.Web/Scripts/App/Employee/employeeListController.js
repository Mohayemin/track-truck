employeeModule.controller('employeeListController', [
    '$scope',
    'employeeService',
    'url',
    function($scope,
        employeeService,
        url) {
        $scope.url = url;

        $scope.loadEmployees = function() {
            employeeService.getAll().then(function(employees) {
                $scope.employees = employees;
            }).catch(function() {
                console.error('could not load employees');
            });
        };

        $scope.loadEmployees();
    }
]);
