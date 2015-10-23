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
                    var reportRows = [];
                    var reportedUserIds = {};

                    rows.forEach(function (row) {
                        return reportedUserIds[row.UserId] = true;
                    });

                    userService.getIndexedUsers().then(function (userIndex) {
                        rows.forEach(function (row) {
                            row.User = userIndex[row.UserId];
                            reportRows.push(row);
                        });

                        for (var userId in userIndex) {
                            if (!reportedUserIds[userId]) {
                                reportRows.push({
                                    UserId: userId,
                                    User : userIndex[userId]
                                });
                            }        
                        }
                    });

                    return reportRows;
                });
            }
        };

        return service;
    }
]);