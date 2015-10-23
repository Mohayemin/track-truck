utilModule.directive('listButton', [
    function () {
        return {
            templateUrl: '/Scripts/Util/listButton.html',
            replace: true,
            scope: {
                module: '=',
            },
            controller: [
                '$scope',
                'url',
                function ($scope,
                    url) {
                    $scope.href = url.route($scope.module, 'list');
                }
            ]
        };
    }
]);