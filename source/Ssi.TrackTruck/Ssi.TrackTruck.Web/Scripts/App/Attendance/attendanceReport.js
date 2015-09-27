attendanceModule.directive('attendanceReport', [
    'url',
    function (
        url
        ) {
        var directive = {
            templateUrl: url.template('Attendance', 'attendanceReport'),
            scope: {},
            controller: [
                '$scope',
                'attendanceService',
                function ($scope,
                    attendanceService
                ) {
                    $scope.filter = {
                        fromDate: {},
                        toDate: {}
                    };

                    $scope.loadReport = function () {
                        attendanceService.getReport($scope.filter).then(function(data) {
                            $scope.reportRows = data;
                        });
                    };
                }
            ]
        };

        return directive;
    }
]);