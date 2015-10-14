clientModule.controller('clientEditController', [
    '$scope',
    '$routeParams',
    'clientService',
    '$location',
    function ($scope,
        $routeParams,
        clientService,
        $location) {

        clientService.get($routeParams['id']).then(function (client) {
            $scope.request = JSON.parse(JSON.stringify(client));
        });

        $scope.save = function() {
            $location.url('client/list');
        }
    }
]);