clientModule.directive('clientSelect', [
    'clientService',
    function (clientService) {
        return {
            templateUrl: '/Scripts/App/Client/clientSelect.html',
            scope: {
                ngModel: '='
            },
            controller: [
                '$scope',
                function($scope) {
                    clientService.getAll().then(function(clients) {
                        if (clients && clients.length) {
                            $scope.ngModel = clients[0];
                        }

                        $scope.clients = clients;
                    }).catch(function() {
                        console.error('could not load clients');
                    });
                }
            ]
        };
    }
]);