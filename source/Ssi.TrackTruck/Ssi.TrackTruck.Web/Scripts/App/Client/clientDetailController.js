clientModule.controller('clientDetailController', [
    '$scope',
    '$routeParams',
    'clientService',
    'userService',
    function (
        $scope
        , $routeParams
        , clientService
        , userService) {
        function init(client) {
            $scope.client = client;

            userService.getIndexedUsers().then(function (userIndex) {
                $scope.client.Branches.forEach(function (branch) {
                    branch.CustodianUser = userIndex[branch.CustodianUserId];
                });
            });


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
