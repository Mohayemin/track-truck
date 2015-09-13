trackTruck.factory('warehouseService', [
    'repository',
    warehouseService
]);

function warehouseService(repository) {
    return {
        getAll: function() {
            return repository.get('Warehouse', 'All');
        }
    };
}