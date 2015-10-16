navigationModule.provider('navs', [
    function () {
        this.$get = [
            'url',
            'userRoles',
            'signedInUser',
            function (
                url
                , roles
                , signedInUser) {
                var newTripGroup = {
                    text: 'New Order Trip',
                    navs: [
                        {
                            url: url.route('trip', 'order'),
                            iconClass: 'fa fa-fw fa-lg fa-plus',
                            text: 'New Trip',
                            roles: [roles.encoder]
                        }, {
                            url: url.route('trip', 'transactions'),
                            iconClass: 'fa fa-fw fa-lg fa-cubes',
                            text: 'Transactions',
                            roles: [roles.admin, roles.branchCustodian]
                        }
                    ]
                };
                var databaseGroup = {
                    text: 'Database',
                    roles: [roles.admin],
                    navs: [
                        {
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
                        }
                    ]
                };
                var reportsGroup = {
                    text: 'Reports',
                    roles: [roles.admin],
                    navs: [
                        {
                            url: url.route('trip', 'report'),
                            iconClass: 'fa fa-fw fa-lg fa-cubes',
                            text: 'Trips'
                        }, {
                            url: url.route('attendance', 'report'),
                            iconClass: 'fa fa-fw fa-lg fa-group',
                            text: 'Attendence'
                        }
                    ]
                };
                var userGroup = {
                    text: '',
                    navs: [
                        {
                            url: url.route('auth', 'changepassword'),
                            iconClass: 'fa fa-fw fa-lg fa-key',
                            text: 'Change Password'
                        }, {
                            url: url.server('auth', 'signout'),
                            iconClass: 'fa fa-fw fa-lg fa-sign-out',
                            text: 'Sign Out'
                        }
                    ]
                };
                var navGroups = [
                    newTripGroup, databaseGroup, reportsGroup, userGroup
                ];

                signedInUser.load().then(function () {
                    userGroup.text = signedInUser.FullName;
                });

                return {
                    groups: navGroups
                };
            }
        ];
    }
]);