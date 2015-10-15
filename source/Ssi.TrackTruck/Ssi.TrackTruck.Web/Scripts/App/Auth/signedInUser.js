authModule.factory('signedInUser', [
    'repository',
    function(repository) {
        var _loadPromise;

        var _user = {
            load: function() {
                _loadPromise = _loadPromise || repository.get('auth', 'getSignedInUser').then(function (user) {
                    for (var prop in user) {
                        _user[prop] = user[prop];
                    }

                    return _user;
                });

                return _loadPromise;
            }
        };

        return _user;
    }
]);