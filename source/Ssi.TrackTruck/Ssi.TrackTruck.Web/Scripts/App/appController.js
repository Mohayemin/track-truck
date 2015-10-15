appModule.controller('appController', [
    '$scope',
    'url',
    function (
        $scope
        , url) {
        $scope.url = url;

        $scope.$on('$routeChangeSuccess', function (event, current) {
            $scope.title = current.$$route.title;
        });
    }
]);