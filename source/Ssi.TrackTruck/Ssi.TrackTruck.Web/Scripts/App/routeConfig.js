appModule.config([
    '$routeProvider',
    'urlProvider',
    function ($routeProvider, urlProvider) {
        function capitalizeFirstLetter(str) {
            return str.charAt(0).toUpperCase() + str.slice(1);
        }

        function defaultRoute(module, feature, id, title) {
            return {
                id: id,
                title: title,
                templateUrl: urlProvider.template(module, feature),
                controller: feature + 'Controller',
                caseInsensitiveMatch: true
            };
        }

        function addRoute(module) {
            var cappedModule = capitalizeFirstLetter(module);
            return {
                id: module.toLowerCase() + 'add',
                title: 'Add ' + module,
                templateUrl: urlProvider.template(module, 'add' + cappedModule),
                controller: 'add' + cappedModule + 'Controller',
                caseInsensitiveMatch: true
            };
        }

        function listRoute(module) {
            return {
                id: module + 'list',
                title: 'List of ' + module + 's',
                templateUrl: urlProvider.template(module, module + 'List'),
                controller: module + 'ListController',
                caseInsensitiveMatch: true
            };
        }

        function editRoute(module) {
            return {
                id: module + 'Edit',
                title: 'Edit ' + module,
                templateUrl: urlProvider.template(module, module + 'Edit'),
                controller: module + 'EditController',
                caseInsensitiveMatch: true
            };
        }

        ['truck', 'client', 'user', 'employee'].forEach(function (module) {
            $routeProvider
                .when('/' + module + '/add', addRoute(module))
                .when('/' + module + '/list', listRoute(module))
                .when('/' + module + '/:id/edit', editRoute(module));
        });

        $routeProvider
            .when('/', {
                title: 'Home',
                templateUrl: urlProvider.template('', 'home'),
            })
            .when('/client/:id', defaultRoute('client', 'clientDetail', 'clientdetail', 'Client Detail'))
            .when('/trip/order', defaultRoute('trip', 'orderTrip', 'ordertrip', 'Order a Trip'))
            .when('/trip/transactions', defaultRoute('trip', 'tripTransaction', 'triptransaction', 'Upcoming Transactions'))
            .when('/trip/report', defaultRoute('trip', 'tripReport', 'tripreport', 'Trip Report'))
            .when('/auth/changepassword', defaultRoute('auth', 'changePassword', 'changepassword', 'Change Password'))
            .when('/attendance/report', defaultRoute('attendance', 'attendanceReport', 'attendancereport', 'Attendance Report'))
            .otherwise({ redirectTo: '/' });
    }
]);