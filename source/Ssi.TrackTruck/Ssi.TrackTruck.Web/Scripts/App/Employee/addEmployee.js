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
                    $scope.designations = {
                        values: [designation.driver, designation.helper],
                        selected: ''
                    };

                    $scope.add = function () {
                        if (employeeService.isDesignationEmpty($scope.designations.selected)) {
                            console.log('designation cannot be empty');
                            return;
                        }
                        $scope.request.Designation = $scope.designations.selected;
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