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
                function ($scope
                    , $location
                    ) {
                    $scope.url = url;
                    $scope.activeGroup = null;
                    $scope.activeNav = null;



                    $scope.navGroups = [
                        {
                            text: 'New Order Trip',
                            navs: [{
                                url: url.route('trip', 'order'),
                                iconClass: 'fa fa-fw fa-lg fa-plus',
                                text: 'New Trip'
                            }, {
                                url: url.route('trip', 'transactions'),
                                iconClass: 'fa fa-fw fa-lg fa-cubes',
                                text: 'Transactions'
                            }],
                        }, {
                            text: 'Database',
                            navs: [{
                                url: url.route('employee', 'list'),
                                iconClass: 'fa fa-fw fa-lg fa-group',
                                text: 'Employees'
                            }, {
                                url: url.route('truck', 'list'),
                                iconClass: 'fa fa-fw fa-lg fa-truck',
                                text: 'Trucks'
                            }, {
                                url: url.route('client', 'list'),
                                iconClass: 'fa fa-fw fa-lg fa-shopping-cart',
                                text: 'Clients'
                            }, {
                                url: url.route('warehouse', 'list'),
                                iconClass: 'fa fa-fw fa-lg fa-home',
                                text: '3PW'
                            }, {
                                url: url.route('user', 'list'),
                                iconClass: 'fa fa-fw fa-lg fa-user',
                                text: 'Users'
                            }],
                        }, {
                            text: 'Reports',
                            navs: [{
                                url: url.route('trip', 'report'),
                                iconClass: 'fa fa-fw fa-lg fa-cubes',
                                text: 'Trips'
                            }, {
                                url: url.route('attendence', 'report'),
                                iconClass: 'fa fa-fw fa-lg fa-group',
                                text: 'Attendence'
                            }],
                        }, {
                            text: $scope.username,
                            navs: [{
                                url: url.route('auth', 'changepassword'),
                                iconClass: 'fa fa-fw fa-lg fa-key',
                                text: 'Change Password'
                            }, {
                                url: url.server('auth', 'signout'),
                                iconClass: 'fa fa-fw fa-lg fa-sign-out',
                                text: 'Sign Out'
                            }],
                        }
                    ];

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

                    setActiveNav();
                    $scope.$on('$locationChangeSuccess', setActiveNav);
                }
            ]
        };
    }
]);