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
                    $scope.fromDate = {};
                    $scope.toDate = {};

                    console.log(attendanceService);
                }
            ]
        };

        return directive;
    }
]);