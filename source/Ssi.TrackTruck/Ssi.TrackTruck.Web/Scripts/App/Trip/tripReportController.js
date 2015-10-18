tripModule.controller('tripReportController', [
    '$scope',
    'wellKnownDateTime',
    'tripService',
    function (
        $scope
        , wellKnownDateTime
        , tripService) {

        $scope.filter = {
            fromDate: wellKnownDateTime.yesterday(),
            toDate: wellKnownDateTime.tomorrow()
        };

        $scope.loadReport = function () {
            tripService.getReport($scope.filter).then(function (reportRows) {
                $scope.reportRows = reportRows;
            });
        };
    }
]);