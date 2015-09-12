trackTruck.controller('clientController', [
    '$scope',
    'clientService',
    clientController
]);

function clientController($scope, clientService) {
    clientService.getAllSummary().then(function(clients) {
        $scope.clients = clients;
    }).catch(function() {
        console.error('could not load clients');
    });
}