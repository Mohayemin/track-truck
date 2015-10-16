utilModule.filter('philDate', [
    '$filter',
    function ($filter) {
        return function(input) {
            return $filter('date')(input, 'EEE dd MMM, yyyy hh:mm a', '+0800');
        }
    }
]);