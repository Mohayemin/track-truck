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
                            roles: [roles.encoder, roles.admin],
                            highLightRoutes: ['triporder']
                        }
                    ]
                };
                var databaseGroup = {
                    text: 'Database',
                    roles: [roles.admin],
                    navs: [
                        {
                            url: url.route('employee', 'list'),
                            iconClass: 'fa fa-fw fa-lg fa-credit-card',
                            text: 'Employees',
                            highLightRoutes: ['employeelist', 'employeeadd', 'employeeedit']
                        }, {
                            url: url.route('truck', 'list'),
                            iconClass: 'fa fa-fw fa-lg fa-truck',
                            text: 'Trucks',
                            highLightRoutes: ['trucklist', 'truckadd', 'truckedit']
                        }, {
                            url: url.route('client', 'list'),
                            iconClass: 'fa fa-fw fa-lg fa-black-tie',
                            text: 'Clients',
                            highLightRoutes: ['clientlist', 'clientadd', 'clientedit']
                        }, {
                            url: url.route('user', 'list'),
                            iconClass: 'fa fa-fw fa-lg fa-user',
                            text: 'Users',
                            highLightRoutes: ['userlist', 'usertadd', 'useredit']
                        }
                    ]
                };
                var reportsGroup = {
                    text: 'Reports',
                    navs: [
                        {
                            url: url.route('trip', 'report'),
                            iconClass: 'fa fa-fw fa-lg fa-cubes',
                            text: 'Trips',
                            roles: [roles.encoder, roles.admin],
                            highLightRoutes: ['tripreport']
                        }, {
                            url: url.route('attendance', 'report'),
                            iconClass: 'fa fa-fw fa-lg fa-check-square-o',
                            text: 'Attendance',
                            roles: [roles.admin],
                            highLightRoutes: ['attendancereport', 'tripdetail']
                        }, {
                            url: url.route('salary', 'report'),
                            iconClass: 'fa fa-fw fa-lg fa-money',
                            text: 'Salary',
                            roles: [roles.admin],
                            highLightRoutes: ['employeeSalaryReport']
                        }
                    ]
                };
                var userGroup = {
                    text: '',
                    navs: [
                        {
                            url: url.route('auth', 'changepassword'),
                            iconClass: 'fa fa-fw fa-lg fa-key',
                            text: 'Change Password',
                            highLightRoutes: ['changepassword']
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