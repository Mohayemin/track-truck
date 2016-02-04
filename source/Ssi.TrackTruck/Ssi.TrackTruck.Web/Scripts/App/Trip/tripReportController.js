tripModule.controller('tripReportController', [
    '$scope',
    'wellKnownDateTime',
    'tripService',
    function (
        $scope
        , wellKnownDateTime
        , tripService) {

        function setDateRange(from, to) {
            $scope.filter.fromDate = from;
            $scope.filter.toDate = to;
        }

        $scope.filter = {};
        setDateRange(wellKnownDateTime.yesterday(), wellKnownDateTime.tomorrow());
        
        $scope.setToday = function () {
            setDateRange(wellKnownDateTime.today(), wellKnownDateTime.today());
        };

        $scope.setThisWeek = function () {
            setDateRange(wellKnownDateTime.weekStart(), wellKnownDateTime.weekEnd());
        };

        $scope.setThisMonth = function() {
            setDateRange(wellKnownDateTime.monthStart(), wellKnownDateTime.monthEnd());
        };


        $scope.loadReport = function () {
            tripService.getReport($scope.filter).then(function (trips) {
                $scope.trips = trips;
            });
        };
    }
]);