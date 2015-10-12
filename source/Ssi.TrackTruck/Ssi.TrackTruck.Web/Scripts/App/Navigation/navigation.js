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

                    function setActiveNav() {
                        var activeUrl = url.route($location.url());
                        $scope.activeGroup = null;
                        $scope.activeNav = null;

                        for (var i = 0; i < $scope.navGroups.length && !$scope.activeGroup; i++) {
                            var group = $scope.navGroups[i];
                            for (var j = 0; j < group.navs.length && !$scope.activeGroup; j++) {
                                var nav = group.navs[j];
                                if (nav.url == activeUrl) {
                                    $scope.activeGroup = group;
                                    $scope.activeNav = nav;
                                }
                            }
                        }
                    }

                    $scope.navGroups = navs.groups;

                    setActiveNav();
                    
                    $scope.$on('$locationChangeSuccess', setActiveNav);
                }
            ]
        };
    }
]);