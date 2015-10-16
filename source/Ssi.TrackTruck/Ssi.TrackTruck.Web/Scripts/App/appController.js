appModule.controller('appController', [
    '$scope',
    'userRoles',
    'url',
    function (
        $scope
        , userRoles
        , url) {
        $scope.url = url;
        $scope.roles = userRoles;
    }
]);