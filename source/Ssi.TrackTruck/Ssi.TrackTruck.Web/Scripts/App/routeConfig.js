appModule.config([
    '$routeProvider',
    function ($routeProvider) {
        console.log('configuring routes');
        function createClosedTag(tagName) {
            return '<' + tagName + '>' + '</' + tagName + '>';
        }

        function defaultRoute(directiveTag) {
            return {
                template: createClosedTag(directiveTag),
                caseInsensitiveMatch: true
            };
        }

        $routeProvider
            .when('/', defaultRoute('home'))
            .when('/truck/add', defaultRoute('add-truck'))
            .when('/truck/report', defaultRoute('truck-status-report'))
            .when('/client/list', defaultRoute('client-list'))
            .when('/client/add', defaultRoute('add-client'))
            .when('/trip/order', defaultRoute('order-trip'))
            .when('/user/list', defaultRoute('user-list'))
            .when('/employee/list', defaultRoute('employee-list'))
            .when('/employee/add', defaultRoute('add-employee'))
            .otherwise({redirectTo: '/'})
        ;
    }
]);