utilModule.directive('editButton', [
    function () {
        return {
            templateUrl: '/Scripts/Util/editButton.html',
            replace: true,
            scope: {
                module: '=',
                itemId: '=',
                cssClass: '='
            },
            controller: [
                '$scope',
                'url',
                function ($scope,
                    url) {
                    $scope.href = url.route($scope.module, $scope.itemId, 'edit');
                }
            ]
        };
    }
]);