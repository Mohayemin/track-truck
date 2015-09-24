userModule.directive('addUser', [
    'url',
    function (
        url
        ) {
        var directive = {
            templateUrl: url.template('User', 'addUser'),
            scope: {},
            controller: [
                '$scope',
                'userService',
                'userRoles',
                '$location',
                'globalMessage',
                function ($scope
                    , userService
                    , userRoles
                    , $location
                    , globalMessage) {

                    $scope.userRoles = userRoles;

                    $scope.request = {
                        InitialPassword: userService.generateInitialPassword()
                    };

                    $scope.setUsername = function () {
                        $scope.request.Username = ($scope.request.FirstName || '') +
                            ($scope.request.LastName || '');
                    };

                    $scope.add = function () {
                        globalMessage.info('adding user', 0);
                        userService.add($scope.request).then(function () {
                            $location.url('user/list');
                            globalMessage.success('user added');
                        }).catch(function (message) {
                            globalMessage.error(message);
                        });
                    };
                }
            ]
        };

        return directive;
    }
]);