clientModule.directive('clientDetail', [
    'url',
    '$window',
    function (url,
        $window) {
        return {
            templateUrl: url.template('Client', 'clientDetail'),
            scope: {},
            controller: [
                '$scope',
                '$routeParams',
                'clientService',
                function ($scope, $routeParams, clientService) {
                    function init(client) {
                        $scope.client = client;

                        $scope.deleteClient = function () {
                            if ($window.confirm('Are you sure you want to delete this client?')) {
                                clientService.delete($scope);
                            }
                        };
                    }

                    clientService.get($routeParams['id']).then(function(client) {
                        init(client);
                    });
                }
            ]
        }
    }
]);