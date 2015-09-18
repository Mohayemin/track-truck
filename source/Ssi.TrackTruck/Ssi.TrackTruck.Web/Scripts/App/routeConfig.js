trackTruck.config([
    '$routeProvider',
    function ($routeProvider) {
        console.log('configuring routes');
        function createClosedTag(tagName) {
            return '<' + tagName + '>' + '</' + tagName + '>';
        }

        $routeProvider
            .when('/hello', {
                template: '<div>hello</div>',
                caseInsensitiveMatch: true
            }).when('/truck/add', {
                template: createClosedTag('add-truck'),
                caseInsensitiveMatch: true
            }).when('/truck/report', {
                template: createClosedTag('truck-status-report'),
                caseInsensitiveMatch: true
            });
    }
]);