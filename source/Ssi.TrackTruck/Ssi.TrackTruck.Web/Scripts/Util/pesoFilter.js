utilModule.filter('peso', [
    '$filter',
    function ($filter) {
        var currency = $filter('currency');
        return function(val) {
            return currency(val, '₱');
        };
    }
])