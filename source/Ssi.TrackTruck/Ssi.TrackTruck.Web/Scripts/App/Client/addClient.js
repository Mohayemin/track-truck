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
                $scope.model = {
                    Branches: [{}]
                };
                $scope.add = function () {
                    clientService.add($scope.model);
                };

                $scope.addBranch = function() {
                    $scope.model.Branches.push({});
                };
            }
        ]
    };
}