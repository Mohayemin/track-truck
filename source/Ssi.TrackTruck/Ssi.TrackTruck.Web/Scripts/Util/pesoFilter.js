utilModule.filter('peso', [
    function () {
        return function (val) {
            return isNaN(parseFloat(val)) ? '' : val + ' ₱';
        };
    }
])