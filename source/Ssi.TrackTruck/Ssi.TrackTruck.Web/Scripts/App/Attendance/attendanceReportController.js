attendanceModule.controller('attendanceReportController', [
    '$scope',
    'attendanceService',
    'wellKnownDateTime',
    function($scope,
        attendanceService,
        wellKnownDateTime
    ) {
        var today = new Date();
        var yesterday = new Date();
        today.setHours(0, 0, 0, 0);
        yesterday.setDate(yesterday.getDate() - 1);
        yesterday.setHours(0, 0, 0, 0);

        var filter = {
            fromDate: yesterday,
            toDate: today
        };

        $scope.filter = filter;

        $scope.loadReport = function() {
            if (filter.fromDate <= filter.toDate) {
                attendanceService.getReport($scope.filter).then(function(data) {
                    $scope.reportRows = data;
                });

                var current = new Date(filter.fromDate);
                var end = new Date(filter.toDate);
                $scope.dates = [];
                while (current <= end) {
                    $scope.dates.push({
                        value: wellKnownDateTime.formatDate(current),
                        label: wellKnownDateTime.formatDate(current, 'd-MMM-yy'),
                    });
                    current.setDate(current.getDate() + 1);
                }
            }
        };
    }
]);