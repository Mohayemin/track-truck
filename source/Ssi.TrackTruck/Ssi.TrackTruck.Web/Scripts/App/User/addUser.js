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
                'globalMessage',
                function ($scope
                    , userService
                    , globalMessage) {

                    $scope.request = {
                        
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