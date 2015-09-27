attendanceModule.factory('attendanceService', [
    'repository',
    function (
        repository
        ) {
        var service = {
            getReport: function(filter) {
                return repository.post('Attendance', 'Report', filter);
            }
        };

        return service;
    }
]);