trackTruck.controller('userController', [
    '$scope',
    userController
]);

function userController($scope) {
    $scope.users = [
        { Username: 'Admin', Role: 'admin' },
        { Username: 'Store-1', Role: 'store' }
    ];
}