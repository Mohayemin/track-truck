utilModule.factory('wellKnownDateTime', [
    '$filter',
    function ($filter) {
        return {
            formatDate: function(date) {
                return $filter('date')(date, 'yyyy-MM-dd');
            }
        };
    }
]);