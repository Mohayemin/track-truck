attendanceModule.controller('attendanceReportController', [
    '$scope',
    'attendanceService',
    'wellKnownDateTime',
    function($scope,
        attendanceService,
        wellKnownDateTime
    ) {
       var filter = {
            fromDate: wellKnownDateTime.yesterday(),
            toDate: wellKnownDateTime.today()
        };

        $scope.filter = filter;

        $scope.loadReport = function() {
            if (filter.fromDate <= filter.toDate) {
                $scope.promise = attendanceService.getReport($scope.filter).then(function(data) {
                    $scope.reportRows = data;
                });

                var current = new Date(filter.fromDate);
                var end = new Date(filter.toDate);
                $scope.dates = [];
                while (current <= end) {
                    $scope.dates.push({
                        value: wellKnownDateTime.formatDateInt(current),
                        label: wellKnownDateTime.formatDate(current, 'd-MMM-yy'),
                    });
                    current.setDate(current.getDate() + 1);
                }
            }
        };
    }
]);