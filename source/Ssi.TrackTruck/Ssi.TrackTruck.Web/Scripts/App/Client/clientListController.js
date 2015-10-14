clientModule.controller('clientListController', [
    '$scope',
    'clientService',
    function($scope,
        clientService) {
        clientService.getAll().then(function(clients) {
            $scope.clients = clients;
        });
    }
]);
