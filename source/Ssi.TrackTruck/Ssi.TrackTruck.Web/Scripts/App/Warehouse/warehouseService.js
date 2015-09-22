warhouseModule.factory('warehouseService', [
    'repository',
    '$q',
    function warehouseService(repository,
        $q) {
        var _loadPromise = null;
        var _warehouses = [];

        var service = {
            getAll: function (force) {
                if (!_loadPromise || force) {
                    _loadPromise = repository.get('Warehouse', 'All').then(function(warehouses) {
                        _warehouses.length = 0;
                        _warehouses.push.apply(_warehouses, warehouses);
                        return _warehouses;
                    });
                }
                return _loadPromise;
            },
            add: function (request) {
                return repository.post('Warehouse', 'Add', request).then(function(response) {
                    if (response.IsError) {
                        return $q.reject(response.Message);
                    }

                    _warehouses.push(response.Data);
                    return response.Data;
                });
            }
        };

        return service;
    }
]);
