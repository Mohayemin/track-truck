utilModule.factory('wellKnownDateTime', [
    '$filter',
    function ($filter) {
        var dateFilter = $filter('date');
        var service = {
            formatIso: function (date) {
                return date.toISOString();
            },
            formatDate: function (date, format) {
                return dateFilter(date, format || 'yyyy-MM-dd');
            },
            formatDateInt: function(date) {
                return dateFilter(date, 'yyyyMMdd');
            },
            today: function() {
                var today = new Date();
                today.setHours(0, 0, 0, 0);
                return today;
            },
            yesterday: function() {
                var yesterday = service.today();
                yesterday.setDate(yesterday.getDate() - 1);
                return yesterday;
            },
            tomorrow: function() {
                var yesterday = service.today();
                yesterday.setDate(yesterday.getDate() + 1);
                return yesterday;
            }
        };
        return service;
    }
]);