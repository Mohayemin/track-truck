clientModule.factory('clientService', [
    'repository',
    function clientService(repository) {
        var _clients = [];
        var _loaded = false;

        return {
            load: function() {
                return repository.get('Client', 'All').then(function (clients) {
                    _clients = clients;
                    _loaded = true;
                    return clients;
                });
            },
            getAll: function () {
                if (!_loaded) {
                    this.load();
                }
                return _clients;
            },
            getAllSummary: function () {
                return repository.get('Client', 'AllSummary');
            },
            add: function (request) {
                return repository.post('Client', 'Add', request).then(function(client) {
                    _clients.push(client);
                });
            }
        };
    }
]);