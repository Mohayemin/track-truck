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
            .when('/truck/list', defaultRoute('truck-list'))
            .when('/client/list', defaultRoute('client-list'))
            .when('/client/add', defaultRoute('add-client'))
            .when('/client/:id', defaultRoute('client-detail'))
            .when('/trip/order', defaultRoute('order-trip'))
            .when('/trip/report', defaultRoute('trip-report'))
            .when('/user/list', defaultRoute('user-list'))
            .when('/user/add', defaultRoute('add-user'))
            .when('/employee/list', defaultRoute('employee-list'))
            .when('/employee/add', defaultRoute('add-employee'))
            .when('/warehouse/add', defaultRoute('add-warehouse'))
            .when('/warehouse/list', defaultRoute('warehouse-list'))
            .when('/auth/changepassword', defaultRoute('change-password'))
            .when('/attendance/report', defaultRoute('attendance-report'))
            .otherwise({ redirectTo: '/' })
        ;
    }
]);