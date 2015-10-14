appModule.config([
    '$routeProvider',
    'urlProvider',
    function ($routeProvider, urlProvider) {
        function capitalizeFirstLetter(str) {
            return str.charAt(0).toUpperCase() + str.slice(1);
        }

        function defaultRoute(module, feature) {
            return {
                templateUrl: urlProvider.template(module, feature),
                controller: feature + 'Controller',
                caseInsensitiveMatch: true
            };
        }

        function addRoute(module) {
            var cappedModule = capitalizeFirstLetter(module);
            return {
                templateUrl: urlProvider.template(module, 'add' + cappedModule),
                controller: 'add' + cappedModule + 'Controller',
                caseInsensitiveMatch: true
            };
        }

        function listRoute(module) {
            return {
                templateUrl: urlProvider.template(module, module + 'List'),
                controller: module + 'ListController',
                caseInsensitiveMatch: true
            };
        }

        function editRoute(module) {
            return {
                templateUrl: urlProvider.template(module, module + 'Edit'),
                controller: module + 'EditController',
                caseInsensitiveMatch: true
            };
        }

        ['truck', 'client', 'user', 'employee', 'warehouse'].forEach(function (module) {
            $routeProvider
                .when('/' + module + '/add', addRoute(module))
                .when('/' + module + '/list', listRoute(module))
                .when('/' + module + '/:id/edit', editRoute(module));
        });

        $routeProvider
            .when('/', {
                templateUrl: urlProvider.template('', 'home')
            })
            .when('/client/:id', defaultRoute('client', 'clientDetail'))
            .when('/trip/order', defaultRoute('trip', 'orderTrip'))
            .when('/trip/transactions', defaultRoute('trip', 'tripTransaction'))
            .when('/auth/changepassword', defaultRoute('auth', 'changePassword'))
            .when('/attendance/report', defaultRoute('attendance', 'attendanceReport'))
            .otherwise({ redirectTo: '/' });
    }
]);