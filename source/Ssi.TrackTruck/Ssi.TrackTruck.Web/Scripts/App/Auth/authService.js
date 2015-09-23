authModule.factory('authService', [
    'repository',
    function (repository) {
        return {
            changePassword: function (request) {
                return repository.post('Auth', 'ChangePassword', request);
            }
        };
    }
]);