﻿utilModule.directive('detailButton', [
    function () {
        return {
            templateUrl: '/Scripts/Util/detailButton.html',
            replace: true,
            scope: {
                module: '=',
                itemId: '=',
            },
            controller: [
                '$scope',
                'url',
                function ($scope,
                    url) {
                    $scope.href = url.route($scope.module, $scope.itemId);
                }
            ]
        };
    }
]);