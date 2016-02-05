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
                $scope.total = {
                    DeliveredNumberOfDrops: 0,
                    TotalNumberOfDrops: 0,
                    TotalNumberOfBoxes: 0,
                    DeliveredNumberOfBoxes: 0,
                    RejectedNumberOfBoxes: 0
                };

                $scope.trips = trips;
                angular.forEach(trips, function (trip) {
                    $scope.total.DeliveredNumberOfDrops += trip.DeliveredNumberOfDrops;
                    $scope.total.TotalNumberOfDrops += trip.TotalNumberOfDrops;
                    $scope.total.TotalNumberOfBoxes += trip.TotalNumberOfBoxes;
                    $scope.total.DeliveredNumberOfBoxes += trip.DeliveredNumberOfBoxes;
                    $scope.total.RejectedNumberOfBoxes += trip.RejectedNumberOfBoxes;
                })
            });
        };
    }
]);