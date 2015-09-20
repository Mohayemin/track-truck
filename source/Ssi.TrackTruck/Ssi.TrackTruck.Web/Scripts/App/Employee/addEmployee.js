employeeModule.directive('addEmployee', [
    'url',
    function(url) {
        return {
            templateUrl: url.template('Employee', 'addEmployee'),
            scope: {},
            controller: [
                '$scope',
                'employeeService',
                'designation',
                function ($scope, employeeService, designation) {
                    $scope.request = {
                        Designation: designation.driver
                    };

                    $scope.add= function() {
                        employeeService.add($scope.request).then(function (response) {
                            if (response.IsError) {
                                console.error('could not add employee');
                            }
                        }).catch(function () {
                            console.error('could not add employee');
                        });
                    }
                }
            ]
        };
    }
]);