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
                    $scope.addRequest = {};

                    $scope.loadClients = function () {
                        clientService.getAllSummary().then(function(clients) {
                            $scope.clients = clients;
                        }).catch(function() {
                            console.error('could not load clients');
                        });
                    };

                    $scope.loadClients();
                }
            ]
        };
    }
]);