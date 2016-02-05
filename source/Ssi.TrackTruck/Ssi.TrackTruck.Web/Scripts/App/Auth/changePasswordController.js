authModule.controller('changePasswordController', [
    '$scope',
    'authService',
    '$location',
    'globalMessage',
    function($scope,
        authService,
        $location,
        globalMessage) {
        $scope.request = {};

        $scope.changePassword = function() {
            authService.changePassword($scope.request).then(function(response) {
                if (!response.IsError) {
                    $location.url('/');
                    globalMessage.success(response.Message);
                } else {
                    globalMessage.error(response.Message);
                }
            }).catch(function(message) {
                globalMessage.error(message);
            });
        };
    }
]);