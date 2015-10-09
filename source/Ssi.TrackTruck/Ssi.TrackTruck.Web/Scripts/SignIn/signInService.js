signInModule.factory('signInService', [
    'repository',
    function (repository) {
        function signIn(request) {
            return repository.post('Auth', 'SignIn', request);
        }

        return {
            signIn: signIn
        };
    }
]);