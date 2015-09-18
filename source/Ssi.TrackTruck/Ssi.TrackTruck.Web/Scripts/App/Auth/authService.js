trackTruck.factory('authService', [
    'repository',
    authService
]);

function authService(repository) {
    function getUserList() {
        return repository.get('Auth', 'GetUserList');
    }

    return {
        getUserList: getUserList
    };
}