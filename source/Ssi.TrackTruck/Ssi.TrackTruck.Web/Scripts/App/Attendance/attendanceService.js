attendanceModule.factory('attendanceService', [
    'repository',
    'userService',
    'wellKnownDateTime',
    function (
        repository,
        userService,
        wellKnownDateTime
        ) {
        var service = {
            getReport: function (filter) {
                var formattedFilter = {
                    fromDate: wellKnownDateTime.formatIso(filter.fromDate),
                    toDate: wellKnownDateTime.formatIso(filter.toDate)
                };
                return repository.post('Attendance', 'Report', formattedFilter).then(function (rows) {
                    userService.getIndexedUsers().then(function(userIndex) {
                        rows.forEach(function (row) {
                            row.FullName = (userIndex[row.UserId] || {}).FullName;
                        });
                    });

                    return rows;
                });
            }
        };

        return service;
    }
]);