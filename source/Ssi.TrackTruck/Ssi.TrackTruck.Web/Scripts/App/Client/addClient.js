clientModule.directive('addClient', [
    'url', '_',
    function (url, _) {
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
                        clientService.add($scope.request).then(function (response) {
                            if (response.IsError) {
                                console.error('could not add client');
                            }
                        }).catch(function () {
                            console.error('could not add client');
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