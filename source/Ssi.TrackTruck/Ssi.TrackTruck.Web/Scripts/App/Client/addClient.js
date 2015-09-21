clientModule.directive('addClient', [
    'url',
    '_',
    '$location',
    function (url,
        _,
        $location) {
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
                        clientService.add($scope.request).then(function () {
                            $location.url('client/list');
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