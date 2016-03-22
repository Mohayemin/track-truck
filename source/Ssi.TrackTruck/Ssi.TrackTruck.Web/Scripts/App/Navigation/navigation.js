navigationModule.directive('navigation', [
    'url',
    function (url) {
        return {
            templateUrl: url.template('Navigation', 'navigation'),
            scope: {
                username: '='
            },
            controller: [
                '$scope',
                '$location',
                'navs',
                function ($scope
                    , $location
                    , navs
                    ) {
                    $scope.url = url;
                    $scope.activeGroup = null;
                    $scope.activeNav = null;
                    $scope.navCollapsed = true;

                    function setActiveNav(event, current) {
                        var routeId = current.$$route.id;

                        $scope.activeGroup = null;
                        $scope.activeNav = null;

                        for (var i = 0; i < $scope.navGroups.length && !$scope.activeGroup; i++) {
                            var group = $scope.navGroups[i];
                            for (var j = 0; j < group.navs.length && !$scope.activeGroup; j++) {
                                var nav = group.navs[j];
                                if (nav.highLightRoutes && nav.highLightRoutes.indexOf(routeId) >= 0) {
                                    $scope.activeGroup = group;
                                    $scope.activeNav = nav;
                                }
                            }
                        }
                    }

                    $scope.navGroups = navs.groups;

                    $scope.$on('$routeChangeSuccess', setActiveNav);
                }
            ]
        };
    }
]);