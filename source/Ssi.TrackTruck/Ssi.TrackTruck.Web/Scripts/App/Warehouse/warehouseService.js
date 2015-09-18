warhouseModule.factory('warehouseService', [
    'repository',
    function warehouseService(repository) {
        return {
            getAll: function() {
                return repository.get('Warehouse', 'All');
            }
        };
    }
]);
