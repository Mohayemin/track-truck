trackTruck.controller('userController', [
    '$scope',
    'authService',
    userController
]);

function userController($scope, authService) {
    authService.getUserList().then(function(users) {
        $scope.users = users;
    });
}