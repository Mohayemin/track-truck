attendanceModule.controller('attendanceReportController', [
    '$scope',
    'attendanceService',
    function($scope,
        attendanceService
    ) {
        var today = new Date();
        var yesterday = new Date();
        yesterday.setDate(yesterday.getDate() - 1);

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

                var current = moment(filter.fromDate);
                var end = moment(filter.toDate);
                $scope.dates = [];
                while (current <= end) {
                    $scope.dates.push({
                        value: current.format('YYYYMMDD'),
                        label: current.format('D-MMM-YY'),
                    });
                    current.add(1, 'd');
                }
            }
        };
    }
]);