utilModule.factory('collection', [
    function() {
        return {
            sum: function(array, property) {
                return array.reduce(function (prev, cur) {
                    return prev + cur[property];
                }, 0);
            },
            count: function (array, property, value) {
                var count = 0;
                for (var i = 0; i < array.length; i++) {
                    if (array[i][property] == value) {
                        count++;
                    }
                }

                return count;
            }
        };
    }
]);