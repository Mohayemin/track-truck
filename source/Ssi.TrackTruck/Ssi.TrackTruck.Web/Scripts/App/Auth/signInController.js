trackTruck.controller('signInController', [
    '$scope',
    signInController
]);

function signInController($scope) {
    $scope.model = {};

    $scope.signIn = function() {
        console.log($scope.model);
    };
}

