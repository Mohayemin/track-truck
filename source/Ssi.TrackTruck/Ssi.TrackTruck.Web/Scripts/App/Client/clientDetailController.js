clientModule.controller('clientDetailController', [
    '$scope',
    '$routeParams',
    'clientService',
    'userService',
    '$window',
    '$location',
    'globalMessage',
    function ($scope, $routeParams, clientService,
        userService,
        $window,
        $location,
        globalMessage) {
        function init(client) {
            $scope.client = client;

            userService.getIndexedUsers().then(function(userIndex) {
                $scope.client.Branches.forEach(function(branch) {
                    branch.CustodianUser = userIndex[branch.CustodianUserId];
                });
            });


            $scope.PickupAddressDisplay = client.Addresses.map(function (address) {
                return address.Text;
            }).join(', ');

            $scope.delete = function() {
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
