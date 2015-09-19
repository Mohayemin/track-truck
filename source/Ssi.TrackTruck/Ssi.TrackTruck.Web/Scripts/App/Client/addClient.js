clientModule.directive('addClient', [
    'url',
    function (url) {
        return {
            templateUrl: url.template('Client', 'addClient'),
            scope: {},
            controller: [
                '$scope',
                'clientService',
                function ($scope, clientService) {
                    $scope.add = function () {
                        clientService.add($scope.addRequest).then(function (response) {
                            if (response.IsError) {
                                console.error('could not add client');
                            } else {
                                $scope.clients.push(response.Data);
                            }

                        }).catch(function () {
                            console.error('could not add client');
                        });
                    };
                }
            ]
        }
    }
]);