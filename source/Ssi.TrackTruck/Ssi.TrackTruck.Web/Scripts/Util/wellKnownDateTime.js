utilModule.factory('wellKnownDateTime', [
    '$filter',
    function ($filter) {
        var dateFilter = $filter('date');
        return {
            formatDate: function (date, format) {
                return dateFilter(date, format || 'yyyy-MM-dd');
            }
        };
    }
]);