clientModule.factory('clientService', [
    'repository',
    function clientService(repository) {
        return {
            getAll: function () {
                return repository.get('Client', 'All');
            },
            getAllSummary: function () {
                return repository.get('Client', 'AllSummary');
            },
            addClient: function (request) {
                return repository.post('Client', 'Add', request);
            }
        };
    }
]);