userModule.factory('userService', [
    'repository',
    '$q',
    function userService(repository
        , $q) {
        var _loadPromise = null;
        var _users = [];
        var _alphaNumbers = 'abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789';

        var service = {
            generateInitialPassword: function () {
                var text = '';
                for (var i = 0; i < 6; i++) {
                    text += _alphaNumbers[Math.floor(Math.random() * _alphaNumbers.length)];
                }

                return text;
            },
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
            add: function (request) {
                return repository.post('User', 'Add', request).then(function (response) {
                    if (response.IsError) {
                        return $q.reject(response.Message);
                    }
                    _users.push(response.Data);
                    return response.Data;
                });
            }
        };

        return service;
    }
]);
