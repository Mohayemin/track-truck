employeeModule.directive('employeeList', [
    'url',
    function(url) {
        return {
            templateUrl: url.template('Employee', 'employeeList'),
            scope: {},
            controller: [
                '$scope',
                'employeeService',
                function ($scope,
                    employeeService) {

                    $scope.loadEmployees = function () {
                        employeeService.getAll().then(function (employees) {
                            $scope.employees = employees;
                        }).catch(function () {
                            console.error('could not load employees');
                        });
                    };

                    $scope.loadEmployees();
                }
            ]
        };
    }
]);