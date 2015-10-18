utilModule.factory('buildIdMap', [
    function () {
        return function buildIdMap(items) {
            var map = {};
            items.forEach(function(item) {
                map[item.Id] = item;
            });

            return map;
        }
    }
]);