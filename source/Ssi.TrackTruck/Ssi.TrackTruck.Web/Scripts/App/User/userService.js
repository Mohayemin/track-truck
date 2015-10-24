userModule.factory('userService', [
    'repository',
    '$q',
    function userService(repository
        , $q) {
        var _loadPromise = null;
        var _users = [];
        var _userById = {};
        var _alphaNumbers = 'abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789';

        function buildIdMap() {
            _userById = {};
            _users.forEach(function(user) {
                _userById[user.Id] = user;
            });
        }

        function getAll(force) {
            if (!_loadPromise || force) {
                _loadPromise = repository.get('User', 'All').then(function (users) {
                    _users.length = 0;
                    _users.push.apply(_users, users);

                    buildIdMap();

                    return _users;
                });
            }
            return _loadPromise;
        }

        var service = {
            generateInitialPassword: function () {
                var text = '';
                for (var i = 0; i < 6; i++) {
                    text += _alphaNumbers[Math.floor(Math.random() * _alphaNumbers.length)];
                }

                return text;
            },
            getAll: getAll,
            add: function (request) {
                var formatterRequest = {
                    FirstName: request.FirstName,
                    LastName: request.LastName,
                    Username: request.Username,
                    InitialPassword: request.InitialPassword,
                    Role: request.Role,
                    ClientId: request.client && request.client.Id,
                    BranchId: request.branch && request.branch.Id
                };
                return repository.post('User', 'Add', formatterRequest).then(function (response) {
                    if (response.IsError) {
                        return $q.reject(response.Message);
                    }
                    _users.push(response.Data);
                    return response.Data;
                });
            },
            getIndexedUsers: function() {
                return service.getAll().then(function() {
                    return _userById;
                });
            },
            get: function (id) {
                return getAll().then(function () {
                    return _.find(_users, { Id: id });
                });
            },
            'delete': function (id) {
                return repository.post('User', 'Delete', { id: id }).then(function (response) {
                    if (!response.IsError) {
                        var index = _.findIndex(_users, { Id: id });
                        if (index >= 0) {
                            _users.splice(index, 1);
                            delete _userById[id];
                        }
                        return response;
                    }

                    return $q.reject(response.Message || response.status || 'could not delete client');
                });
            },
            edit: function (request) {
                return repository.post('User', 'Save', request).then(function (response) {
                    if (response.IsError) {
                        return $q.reject(response.Message || response.Status || 'Could not edit user');
                    }

                    var user = _userById[request.Id];
                    angular.extend(user, response.Data);
                    return user;
                });
            },
            getUsersByRole: function(role) {
                return service.getAll().then(function() {
                    return _.where(_users, { Role: role });
                });
            }
        };

        return service;
    }
]);
