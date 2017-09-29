signInModule.controller('signInController', [
    '$scope',
    '$window',
    'signInService',
    'globalMessage',
    function signInController($scope, $window, signInService, globalMessage) {
        $scope.model = {
			Username: "mohayemin",
			Password: "1"
		};

        $scope.signIn = function() {
            $scope.response = null;
            globalMessage.info('checking...', 0);
            signInService.signIn($scope.model).then(function(response) {
                $scope.message = null;
                $scope.response = response;

                if (!response.IsError) {
                    globalMessage.success(response.Message);
                    $window.location.reload();
                } else {
                    globalMessage.error(response.Message);
                }
            });
        };
    }
]);


