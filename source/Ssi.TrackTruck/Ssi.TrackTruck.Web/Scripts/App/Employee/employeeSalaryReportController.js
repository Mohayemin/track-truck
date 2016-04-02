employeeModule.controller('employeeSalaryReportController', [
    '$scope',
    'wellKnownDateTime',
    'employeeService',
    'collection',
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
            $scope.promise = employeeService.getSalaryReport($scope.filter).then(function (report) {
                $scope.employeeSalaries = report.employees;
                $scope.total = report.total;
            });
        };

        $scope.loadReport();
    }
]);