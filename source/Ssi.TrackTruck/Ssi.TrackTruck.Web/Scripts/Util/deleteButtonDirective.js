utilModule.directive('deleteButton', [
    'url',
    'globalMessage',
    '$location',
    '$window',
    function (url,
        globalMessage,
        $location,
        $window) {
        return {
            templateUrl: '/Scripts/Util/deleteButton.html',
            replace: true,
            scope: {
                module: '=',
                itemId: '=',
                action: '&'
            },
            controller: [
                '$scope',
                function ($scope) {
                    $scope.delete = function () {
                        if ($window.confirm('Are you sure you want to delete this item?')) {
                            $scope.action()($scope.itemId).then(function (response) {
                                if (!response.IsError) {
                                    globalMessage.success(response.Message);
                                    $location.url(url.route($scope.module, 'list'));
                                } else {
                                    globalMessage.error(response.Message);
                                }
                            }).catch(function(message) {
                                globalMessage.error(message);
                            });
                        }
                    }
                }
            ]
        };
    }
]);