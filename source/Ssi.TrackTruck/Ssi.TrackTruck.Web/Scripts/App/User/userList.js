userModule.directive('userList', [
    'url',
    function (url) {
        return {
            templateUrl: url.template('User', 'userList'),
            scope: {},
            controller: [
                '$scope',
                'userService',
                function ($scope, userService) {
                    userService.getAll().then(function (users) {
                        $scope.users = users;
                    });
                }
            ]
        }
    }
]);