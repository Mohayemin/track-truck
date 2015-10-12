appModule.controller('appController', [
    '$scope',
    'url',
    function (
        $scope
        , url) {
        $scope.url = url;
    }
]);