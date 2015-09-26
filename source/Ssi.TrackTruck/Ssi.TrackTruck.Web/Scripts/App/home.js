appModule.directive('home', [
    'url',
    function(url) {
        return {
            templateUrl: url.template('', 'home'),
            scope: {},
            controller: [
                function() {
                }
            ]
        };
    }
])