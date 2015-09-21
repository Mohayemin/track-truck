clientModule.factory('clientService', [
    'repository', '_',
    function clientService(repository, _) {
        var _clients = [];
        var _loaded = false;
        var _loadPromise;

        var service = {
            load: function() {
                return repository.get('Client', 'All').then(function (clients) {
                    _clients.push.apply(_clients, clients);
                    _loaded = true;
                    return clients;
                });
            },
            getAll: function () {
                return _loadPromise.then(function() {
                    return _clients;
                });
            },
            add: function (request) {
                return repository.post('Client', 'Add', request).then(function(client) {
                    _clients.push(client);
                });
            },
            get: function (id) {
                return _loadPromise.then(function() {
                    return _.find(_clients, { Id: id });
                });
            }
        };

        _loadPromise = service.load();

        return service;
    }
]);