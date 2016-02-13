employeeModule.controller('employeeSalaryReportController', [
    '$scope',
    'wellKnownDateTime',
    'employeeService',
    function (
        $scope
        , wellKnownDateTime
        , employeeService) {
        
        function setDateRange(from, to) {
            $scope.filter.fromDate = from;
            $scope.filter.toDate = to;
        }

        $scope.filter = {};
        setDateRange(wellKnownDateTime.yesterday(), wellKnownDateTime.tomorrow());

        $scope.setThisWeek = function () {
            setDateRange(wellKnownDateTime.weekStart(), wellKnownDateTime.weekEnd());
        };

        $scope.setThisMonth = function () {
            setDateRange(wellKnownDateTime.monthStart(), wellKnownDateTime.monthEnd());
        };

        $scope.loadReport = function () {

            employeeService.getSalaryReport($scope.filter).then(function (employeeSalaries) {
                $scope.total = {
                    TotalPayable: 0
                };

                $scope.employeeSalaries = employeeSalaries;
                angular.forEach(employeeSalaries, function (employee) {
                    $scope.total.TotalPayable += employee.TotalPayable;
                });
            });
        };

        $scope.loadReport();
    }
]);