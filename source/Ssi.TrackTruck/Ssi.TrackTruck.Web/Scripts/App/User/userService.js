userModule.factory('userService', [
    'repository',
    '$q',
    function userService(repository
        , $q) {
        var _loadPromise = null;
        var _users = [];

        var service = {
            getAll: function (force) {
                if (!_loadPromise || force) {
                    _loadPromise = repository.get('User', 'All').then(function (users) {
                        _users.length = 0;
                        _users.push.apply(_users, users);
                        return _users;
                    });
                }
                return _loadPromise;
            },
            add: function(request) {
                return repository.post('User', 'Add', request).then(function(response) {
                    if (response.IsError) {
                        return $q.reject(response.Message);
                    }
                    return service.getAll().then(function(users) {
                        var user = response.Data;
                        users.push(user);
                        return user;
                    });
                });
            }
        };

        return service;
    }
]);
