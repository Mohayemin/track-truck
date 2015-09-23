authModule.directive('changePassword', [
    'url',
    '$location',
    function (url, $location) {
        return {
            templateUrl: url.template('Auth', 'changePassword'),
            scope: {},
            controller: [
                '$scope',
                'authService',
                function ($scope, authService) {
                    $scope.request = {};

                    $scope.changePassword = function() {
                        authService.changePassword($scope.request).then(function(response) {
                            if (!response.IsError) {
                                $location.path('/');
                            }
                        });
                    };
                }
            ]
        };
    }
]);