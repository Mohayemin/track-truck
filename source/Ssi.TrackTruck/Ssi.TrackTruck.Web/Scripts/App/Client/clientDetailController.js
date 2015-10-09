clientModule.controller('clientDetailController', [
    '$scope',
    '$routeParams',
    'clientService',
    '$window',
    '$location',
    'globalMessage',
    function($scope, $routeParams, clientService, $window,
        $location,
        globalMessage) {
        function init(client) {
            $scope.client = client;

            $scope.deleteClient = function() {
                if ($window.confirm('Are you sure you want to delete this client?')) {
                    clientService.delete($scope.client).then(function() {
                        $location.url('client/list');
                        globalMessage.warning('client deleted');
                    }).catch(function(message) {
                        globalMessage.error(message);
                    });
                }
            };
        }

        clientService.get($routeParams['id']).then(function(client) {
            init(client);
        });
    }
]);
