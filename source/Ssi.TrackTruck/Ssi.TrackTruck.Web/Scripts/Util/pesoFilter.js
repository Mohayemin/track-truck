utilModule.filter('peso', [
    function () {
        return function(val) {
            return val + ' ₱';
        };
    }
])