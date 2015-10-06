utilModule.directive('deleteButton', [
    'url',
    'repository',
    function(url,
        repository) {
        return {
            templateUrl: '/Scripts/Util/deleteButton.html',
            scope: {
                controller: '=',
                action: '=',
                itemId: '='
            },
            controller: [
                '$scope',
                function ($scope) {
                    $scope.delete = function () {
                        console.log($scope.controller, $scope.action, $scope.itemId);
                        repository.post($scope.controller, $scope.action, $scope.itemId);
                    }
                }
            ]
        };
    }
]);