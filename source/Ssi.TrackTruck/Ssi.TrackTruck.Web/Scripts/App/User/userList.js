userModule.directive('userList', [
    'url',
    function (url) {
        return {
            templateUrl: url.template('User', 'userList'),
            scope: {},
            controller: [
                '$scope',
                'userService',
                'userRoles',
                function ($scope
                    , userService
                    , userRoles) {
                    $scope.roleMap = userRoles.map;
                    $scope.url = url;
                    userService.getAll().then(function (users) {
                        $scope.users = users;
                    });
                }
            ]
        }
    }
]);