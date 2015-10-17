userModule.controller('userListController', [
    '$scope',
    'userService',
    'userRoles',
    function ($scope, userService, userRoles) {
        $scope.roleMap = userRoles.map;
        userService.getAll().then(function(users) {
            $scope.users = users;
        });

        $scope.deleteItem = userService.delete;
    }
]);