trackTruck.config([
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
            .when('/truck/add', defaultRoute('add-truck'))
            .when('/truck/report', defaultRoute('truck-status-report'))
            .when('/client/list', defaultRoute('client-list'))
            .when('/client/add', defaultRoute('add-client'))
        ;
    }
]);