trackTruck.controller('signInController', [
    '$scope',
    'authService',
    signInController
]);

function signInController($scope, authService) {
    $scope.model = {};
    
    $scope.signIn = function () {
        $scope.response = null;
        $scope.message = 'checking...';
        authService.signIn($scope.model).then(function (response) {
            $scope.message = null;
            $scope.response = response;
        });
    };
}

