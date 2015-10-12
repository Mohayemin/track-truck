utilModule.directive('deleteButton', [
    'url',
    'repository',
    'globalMessage',
    '$window',
    function (url,
        repository,
        globalMessage,
        $window) {
        return {
            templateUrl: '/Scripts/Util/deleteButton.html',
            scope: {
                controller: '=',
                list: '=',
                index: '='
            },
            controller: [
                '$scope',
                function ($scope) {
                    $scope.delete = function () {
                        var action = 'Delete';
                        var data = {
                            id: $scope.list[$scope.index].Id
                        };

                        if ($window.confirm('Are you sure you want to delete this item?')) {
                            repository.post($scope.controller, action, data).then(function (response) {
                                if (!response.IsError) {
                                    $scope.list.splice($scope.index, 1);
                                    globalMessage.success(response.Message);
                                } else {
                                    globalMessage.error(response.Message);
                                }
                            }).catch(function (message) {
                                globalMessage.error(message);
                            });
                        }
                    }
                }
            ]
        };
    }
]);