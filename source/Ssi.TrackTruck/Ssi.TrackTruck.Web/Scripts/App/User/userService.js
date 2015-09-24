userModule.factory('userService', [
    'repository',
    function userService(repository) {
        function getAll() {
            return repository.get('User', 'All');
        }

        return {
            getAll: getAll
        };
    }
]);
