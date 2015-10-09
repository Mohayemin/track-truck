attendanceModule.factory('attendanceService', [
    'repository',
    'userService',
    function (
        repository,
        userService
        ) {
        var service = {
            getReport: function (filter) {
                return repository.post('Attendance', 'Report', filter).then(function (rows) {
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