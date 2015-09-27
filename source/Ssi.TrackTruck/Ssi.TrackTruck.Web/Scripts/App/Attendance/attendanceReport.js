attendanceModule.directive('attendanceReport', [
    'url',
    function (
        url
        ) {
        var directive = {
            templateUrl: url.template('Attendance', 'attendanceReport'),
            scope: {},
            controller: [
                'attendanceService',
                function (
                    attendanceService
                    ) {

                    console.log(attendanceService);
                }
            ]
        };

        return directive;
    }
]);