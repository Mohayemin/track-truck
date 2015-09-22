navigationModule.directive('navigation', [
    'url',
    function(url) {
        return {
            templateUrl: url.template('Navigation', 'navigation'),
            scope: {
                username : '='
            },
            controller: [
                '$scope',
                function($scope) {
                    $scope.url = url;
                }
            ]
        };
    }
]);