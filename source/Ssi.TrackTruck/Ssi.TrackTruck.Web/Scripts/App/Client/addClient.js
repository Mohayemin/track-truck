trackTruck.directive('addClient', [
    'url',
    'clientService',
    addClientDirective
]);

function addClientDirective(url, clientService) {
    return {
        templateUrl: url.resolveTemplate('Client', 'addClient'),
        scope: {},
        controller: [
            '$scope',
            function ($scope) {
                $scope.model = {};
                $scope.add = function () {
                    clientService.add($scope.model);
                };
            }
        ]
    };
}