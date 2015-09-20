userModule.factory('userService', [
    'repository',
    function userService(repository) {
        function getUserList() {
            return repository.get('Auth', 'GetUserList');
        }

        return {
            getUserList: getUserList
        };
    }
]);
