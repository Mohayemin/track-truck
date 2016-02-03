clientModule.controller('clientDetailController', [
    '$scope',
    '$routeParams',
    'clientService',
    function (
        $scope
        , $routeParams
        , clientService) {
        function init(client) {
            $scope.client = client;

            $scope.PickupAddressDisplay = client.Addresses.map(function (address) {
                return address.Text;
            }).join(', ');

            $scope.delete = clientService.delete;
        }

        clientService.get($routeParams['id']).then(function (client) {
            init(client);
        });
    }
]);
