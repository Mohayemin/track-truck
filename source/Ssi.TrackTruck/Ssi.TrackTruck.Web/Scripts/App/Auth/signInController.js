trackTruck.controller('signInController', [
    '$scope',
    '$location',
    '$window',
    'authService',
    'url',
    signInController
]);

function signInController($scope, $location, $window, authService, url) {
    $scope.model = {};

    $scope.signIn = function () {
        $scope.response = null;
        $scope.message = 'checking...';
        authService.signIn($scope.model).then(function (response) {
            $scope.message = null;
            $scope.response = response;

            if (!response.IsError) {
                $window.location.href = url();
            }
        });
    };
}

