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
                    fromDate: wellKnownDateTime.formatDate(filter.from),
                    toDate: wellKnownDateTime.formatDate(filter.toDate)
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