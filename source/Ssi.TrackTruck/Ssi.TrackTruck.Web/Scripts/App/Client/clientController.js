trackTruck.controller('clientController', [
    '$scope',
    'clientService',
    clientController
]);

function clientController($scope, clientService) {
    $scope.addRequest = {};

    $scope.addClient = function() {
        clientService.addClient($scope.addRequest).then(function (response) {
            if (response.IsError) {
                console.error('could not add client');
            } else {
                $scope.clients.push(response.Data);
            }

        }).catch(function() {
            console.error('could not add client');
        });
    };

    $scope.loadClients = function() {
        clientService.getAllSummary().then(function (clients) {
            $scope.clients = clients;
        }).catch(function () {
            console.error('could not load clients');
        });
    };

    $scope.loadClients();
}