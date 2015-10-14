utilModule.factory('collection', [
    function() {
        return {
            sum: function(array, property) {
                return array.reduce(function (a, b) {
                    return a + b[property];
                }, 0);
            }
        };
    }
]);