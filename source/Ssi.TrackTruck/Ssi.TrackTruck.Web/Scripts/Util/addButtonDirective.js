utilModule.directive('addButton', [
    function () {
        return {
            templateUrl: '/Scripts/Util/addButton.html',
            scope: {
                module: '=',
            },
            controller: [
                '$scope',
                'url',
                function (
                    $scope,
                    url) {
                    $scope.href = url.route($scope.module, 'add');
                }
            ]
        };
    }
]);