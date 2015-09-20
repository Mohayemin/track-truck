clientModule.directive('clientDetail', [
    'url', '_',
    function (url, _) {
        return {
            templateUrl: url.template('Client', 'clientDetail'),
            scope: {},
            controller: [
                '$scope',
                '$routeParams',
                'clientService',
                function ($scope, $routeParams, clientService) {
                    clientService.get($routeParams['id']).then(function(client) {
                        $scope.client = client;
                    });
                    
                }
            ]
        }
    }
]);