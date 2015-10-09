userModule.controller('userListController', [
    '$scope',
    'userService',
    'userRoles',
    'url',
    function ($scope, userService, userRoles, url) {
        $scope.roleMap = userRoles.map;
        $scope.url = url;
        userService.getAll().then(function(users) {
            $scope.users = users;
        });
    }
]);