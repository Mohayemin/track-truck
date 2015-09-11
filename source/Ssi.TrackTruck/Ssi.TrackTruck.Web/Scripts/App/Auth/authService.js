trackTruck.factory('authService', [
    'repository',
    authService
]);

function authService(repository) {
    function signIn(request) {
        return repository.post('Auth', 'SignIn', request);
    }

    function getUserList() {
        return repository.get('Auth', 'GetUserList');
    }

    return {
        signIn: signIn,
        getUserList: getUserList
    };
}