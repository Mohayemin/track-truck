﻿utilModule.directive('editButton', [
    function () {
        return {
            templateUrl: 'Util/editButton.html',
            replace: true,
            scope: {
                module: '=',
                itemId: '='
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