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
                    $scope.url = url;
                    clientService.getAll().then(function(clients) {
                        $scope.clients = clients;
                    });

                    $scope.loadClients = function () {
                        clientService.load();
                    };
                }
            ]
        };
    }
]);