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
                    $scope.client = clientService.get($routeParams['id']);
                }
            ]
        }
    }
]);