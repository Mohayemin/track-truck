trackTruck.factory('wirehouseService', [
    'repository',
    wirehouseService
]);

function wirehouseService(repository) {
    return {
        getAll: function() {
            return repository.get('Wirehouse', 'All');
        }
    };
}