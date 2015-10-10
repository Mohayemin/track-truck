utilModule.directive('editButton', [
    function() {
        return {
            templateUrl: '/Scripts/Util/editButton.html',
            scope: {
                module: '=',
                model: '='
            },
            controller: [
                '$scope',
                '$location',
                function ($scope,
                    $location) {
                    $scope.edit = function() {
                        $location.url($scope.module + '/' + $scope.model.Id + '/edit');
                    }
                }
            ]
        };
    }
]);