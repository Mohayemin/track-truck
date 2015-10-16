navigationModule.provider('navs', [
    'urlProvider',
    function (url) {
        var newTripGroup = {
            text: 'New Order Trip',
            navs: [
                {
                    url: url.route('trip', 'order'),
                    iconClass: 'fa fa-fw fa-lg fa-plus',
                    text: 'New Trip'
                }, {
                    url: url.route('trip', 'transactions'),
                    iconClass: 'fa fa-fw fa-lg fa-cubes',
                    text: 'Transactions'
                }
            ]
        };
        var databaseGroup = {
            text: 'Database',
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
                    url: url.route('user', 'list'),
                    iconClass: 'fa fa-fw fa-lg fa-user',
                    text: 'Users'
                }
            ]
        };
        var reportsGroup = {
            text: 'Reports',
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

        var provider = {
            groups: navGroups
        };

        angular.extend(this, provider);

        this.$get = [
            'signedInUser',
            function (signedInUser) {
                signedInUser.load().then(function() {
                    userGroup.text = signedInUser.FullName;
                });

                return provider;
            }
        ];
    }
]);