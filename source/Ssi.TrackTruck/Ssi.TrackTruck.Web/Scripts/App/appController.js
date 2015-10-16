appModule.controller('appController', [
    '$scope',
    'url',
    'pageTitle',
    function (
        $scope
        , url
        , pageTitle) {
        $scope.url = url;

        pageTitle.setSuffix(' | SSI Logistics');

        $scope.$on('$routeChangeSuccess', function (event, current) {
            pageTitle.setTitle(current.$$route.title);
        });
    }
]);