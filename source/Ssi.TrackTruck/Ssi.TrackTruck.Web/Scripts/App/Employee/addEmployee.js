employeeModule.directive('addEmployee', [
    'url',
    '$location',
    function (url,
        $location) {
            return {
            templateUrl: url.template('Employee', 'addEmployee'),
            scope: {},
            controller: [
                '$scope',
                'employeeService',
                'designation',
                function ($scope, employeeService, designation) {
                    $scope.designations = {
                        values: [designation.supervisor, designation.driver, designation.helper],
                        selected: ''
                    };

                    function isValid() {
                        if (employeeService.isDesignationEmpty($scope.designations.selected)) {
                            console.log('designation cannot be empty');
                            return false;
                        }
                        $scope.request.Designation = $scope.designations.selected;
                        return true;
                    }

                    $scope.add = function () {
                        if (isValid()) {
                            employeeService.add($scope.request).then(function () {
                                $location.url('employee/list');
                            }).catch(function () {
                                console.error('could not add employee');
                            });
                        }
                    }
                }
            ]
        };
    }
]);