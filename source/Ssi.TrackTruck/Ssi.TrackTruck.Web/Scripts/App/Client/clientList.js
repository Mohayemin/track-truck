clientModule.directive('clientList', [
    'url',
    function(url) {
        return {
            templateUrl: url.template('Client', 'clientList'),
            scope: {},
            controller: [
                '$scope',
                'clientService',
                function($scope,
                    clientService) {

                    $scope.clients = clientService.getAll();

                    $scope.loadClients = function () {
                        clientService.load();
                    };
                }
            ]
        };
    }
]);