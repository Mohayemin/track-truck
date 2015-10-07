clientModule.directive('addClient', [
    'url',
    '_',
    '$location',
    'globalMessage',
    function (url,
        _,
        $location,
        globalMessage) {
        return {
            templateUrl: url.template('Client', 'addClient'),
            scope: {},
            controller: [
                '$scope',
                'clientService',
                function ($scope, clientService) {
                    $scope.request = {
                        Branches : []
                    };

                    $scope.add = function () {
                        globalMessage.info('adding client', 0);
                        clientService.add($scope.request).then(function () {
                            $location.url('client/list');
                            globalMessage.success('client added');
                        }).catch(function (message) {
                            globalMessage.error(message);
                        });
                    };

                    $scope.addBranch = function() {
                        $scope.request.Branches.push({});
                    };

                    $scope.deleteBranch = function(index) {
                        $scope.request.Branches.splice(index, 1);
                    };
                }
            ]
        }
    }
]);