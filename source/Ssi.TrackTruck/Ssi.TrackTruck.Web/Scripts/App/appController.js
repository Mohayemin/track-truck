appModule.controller('appController', [
    '$scope',
    'userRoles',
    'url',
    'pageTitle',
    function (
        $scope
        , url
        , userRoles
        , pageTitle) {
        $scope.url = url;
        $scope.roles = userRoles;
        pageTitle.setSuffix(' | SSI Logistics');

        $scope.$on('$routeChangeSuccess', function (event, current) {
            pageTitle.setTitle(current.$$route.title);
        });
    }
]);