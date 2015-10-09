appModule.config([
    '$routeProvider',
    'urlProvider',
    function ($routeProvider, urlProvider) {
        function capitalizeFirstLetter(str) {
            return str.charAt(0).toUpperCase() + str.slice(1);
        }

        function createClosedTag(tagName) {
            return '<' + tagName + '>' + '</' + tagName + '>';
        }

        function defaultRoute(module, feature) {
            return {
                templateUrl: urlProvider.template(module, feature),
                controller: feature + 'Controller',
                caseInsensitiveMatch: true
            };
        }

        function routeForAdd(module) {
            var cappedModule = capitalizeFirstLetter(module);
            return $routeProvider.when(urlProvider.path(module, 'add'), {
                templateUrl: urlProvider.template(module, 'add' + cappedModule),
                controller: 'add' + cappedModule + 'Controller',
                caseInsensitiveMatch: true
            });
        }

        function routeForList(module) {
            return $routeProvider.when(urlProvider.path(module, 'list'), {
                templateUrl: urlProvider.template(module, module + 'List'),
                controller: module + 'ListController',
                caseInsensitiveMatch: true
            });
        }

        ['truck', 'client', 'user', 'employee', 'warehouse'].forEach(function (module) {
            routeForAdd(module);
        });

        ['truck', 'client', 'user', 'employee', 'warehouse'].forEach(function (module) {
            routeForList(module);
        });

        $routeProvider
            .when('/', {
                templateUrl: urlProvider.template('', 'home')
            })
            .when('/client/:id', defaultRoute('client', 'clientDetail'))
            .when('/trip/order', defaultRoute('trip', 'orderTrip'))
            .when('/auth/changepassword', defaultRoute('auth', 'changePassword'))
            .when('/attendance/report', defaultRoute('attendance', 'attendanceReport'))
            .otherwise({ redirectTo: '/' });
    }
]);