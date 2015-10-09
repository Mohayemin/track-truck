clientModule.controller('clientListController', [
    '$scope',
    'clientService',
    'url',
    function($scope,
        clientService,
        url) {
        $scope.url = url;
        clientService.getAll().then(function(clients) {
            $scope.clients = clients;
        });

        $scope.loadClients = function() {
            clientService.load();
        };
    }
]);
