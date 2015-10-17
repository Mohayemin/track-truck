clientModule.factory('clientService', [
    'repository',
    '_',
    'buildIdMap',
    '$q',
    function clientService(repository,
        _,
        buildIdMap,
        $q) {
        var _clients = [];
        var _clientsById = {};
        var _loadPromise;

        var service = {
            getAll: function () {
                if (!_loadPromise) {
                    _loadPromise = repository.get('Client', 'All').then(function (clients) {
                        _clients.length = 0;
                        _clients.push.apply(_clients, clients);
                        _clientsById = buildIdMap(_clients);

                        _clients.forEach(function (client) {
                            client.BranchesById = buildIdMap(client.Branches);
                        });

                        return _clients;
                    });
                }

                return _loadPromise;
            },
            add: function (request) {
                return repository.post('Client', 'Add', request).then(function (response) {
                    if (response.IsError) {
                        return $q.reject(response.Message || response.Status || 'Could not add client');
                    }
                    var client = response.Data;
                    _clients.push(client);
                    return client;
                });
            },
            edit: function (request) {
                return repository.post('Client', 'Edit', request).then(function (response) {
                    if (response.IsError) {
                        return $q.reject(response.Message || response.Status || 'Could not edit truck');
                    }

                    var client = response.Data;
                    angular.extend(_clientsById[client.Id], client);

                    return client;
                });
            },
            get: function (id) {
                return service.getAll().then(function () {
                    return _clientsById[id];
                });
            },
            'delete': function (client) {
                if (!client) {
                    return $q.reject('client does not exist');
                }
                return repository.post('Client', 'Delete', { id: client.Id }).then(function (response) {
                    if (!response.IsError) {
                        var index = _.findIndex(_clients, { Id: client.Id });
                        if (index >= 0) {
                            _clients.splice(index, 1);
                            delete _clientsById[client.Id];
                        }
                        return response;
                    }

                    return $q.reject(response.Message || response.status || 'could not delete client');
                });
            },
            getIndexedClients: function () {
                return service.getAll().then(function () {
                    return _clientsById;
                });
            }
        };

        return service;
    }
]);