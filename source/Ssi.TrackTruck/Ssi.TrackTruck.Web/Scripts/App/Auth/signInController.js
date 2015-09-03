trackTruck.controller('signInController', [
    '$scope',
    'authService',
    signInController
]);

function signInController($scope, authService) {
    $scope.model = {};

    $scope.signIn = function() {
        authService.signIn($scope.model).then(function(response) {
            console.log(response);
        });
    };
}

