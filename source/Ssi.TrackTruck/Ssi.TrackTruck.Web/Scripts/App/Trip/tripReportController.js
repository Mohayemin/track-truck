tripModule.controller('tripReportController', [
    '$scope',
    'tripService',
    function (
        $scope
        , tripService) {

        $scope.filter = {
            fromDate: {},
            toDate: {}
        };

        $scope.loadReport = function () {
            tripService.getReport($scope.filter).then(function (reportRows) {
                $scope.reportRows = reportRows;
            });
        };

    }
]);