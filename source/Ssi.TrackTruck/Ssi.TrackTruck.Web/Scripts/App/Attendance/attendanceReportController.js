attendanceModule.controller('attendanceReportController', [
    '$scope',
    'attendanceService',
    function($scope,
        attendanceService
    ) {
        var today = moment();
        var yesterday = moment().add(-1, 'd');

        $scope.filter = {
            fromDate: {
                year: yesterday.year(),
                month: yesterday.month(),
                day: yesterday.date()
            },
            toDate: {
                year: today.year(),
                month: today.month(),
                day: today.date()
            }
        };

        $scope.loadReport = function() {
            var fd = $scope.filter.fromDate;
            var td = $scope.filter.toDate;
            var start = moment(new Date(fd.year, fd.month - 1, fd.day));
            var end = moment(new Date(td.year, td.month - 1, td.day));

            if (start <= end) {
                attendanceService.getReport($scope.filter).then(function(data) {
                    $scope.reportRows = data;
                });

                var current = start;
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