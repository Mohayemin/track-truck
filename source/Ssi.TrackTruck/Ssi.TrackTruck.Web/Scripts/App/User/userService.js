﻿userModule.factory('userService', [
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

                        buildIdMap();

                        return _users;
                    });
                }
                return _loadPromise;
            },
            add: function (request) {
                var formatterRequest = {
                    FirstName: request.FirstName,
                    LastName: request.LastName,
                    Username: request.Username,
                    InitialPassword: request.InitialPassword,
                    Role: request.Role,
                    ClientId: request.client.Id,
                    BranchId: request.branch.Id
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
            }
        };

        return service;
    }
]);
