clientModule.factory('clientService', [
    'repository', '_',
    function clientService(repository, _) {
        var _clients = [];
        var _loadPromise;

        var service = {
            load: function () {
                return repository.get('Client', 'All').then(function (clients) {
                    _clients.length = 0;
                    _clients.push.apply(_clients, clients);
                    return clients;
                });
            },
            getAll: function () {
                return _loadPromise.then(function () {
                    return _clients;
                });
            },
            add: function (request) {
                return repository.post('Client', 'Add', request).then(function (client) {
                    _clients.push(client);
                    return client;
                });
            },
            get: function (id) {
                return _loadPromise.then(function () {
                    return _.find(_clients, { Id: id });
                });
            },
            'delete': function (client) {
                return repository.post('Client', 'Delete', { id: client.Id }).then(function (response) {
                    if (!response.IsError) {
                        client.IsDeleted = true;
                    }
                    return response;
                });
            }
        };

        _loadPromise = service.load();

        return service;
    }
]);