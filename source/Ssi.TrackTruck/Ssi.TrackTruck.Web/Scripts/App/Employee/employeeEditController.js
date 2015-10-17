employeeModule.controller('employeeEditController', [
    '$scope',
    '$routeParams',
    'employeeService',
    'designation',
    'globalMessage',
    '$location',
    function ($scope,
        $routeParams,
        employeeService,
        designation,
        globalMessage,
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
            $scope.request.Designation = $scope.designations.selected;
            employeeService.edit($scope.request).then(function () {
                globalMessage.success('Successfully edited');
                $location.url('employee/list');
            }).catch(function (message) {
                globalMessage.error(message);
            });
        }
    }
]);