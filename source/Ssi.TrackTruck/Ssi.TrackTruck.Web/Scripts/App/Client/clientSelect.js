clientModule.directive('clientSelect', [
    'clientService',
    function (clientService) {
        return {
            templateUrl: 'Client/clientSelect.html',
            scope: {
                ngModel: '=',
                autoSelectIndex: '='
            },
            controller: [
                '$scope',
                function($scope) {
                    clientService.getAll().then(function(clients) {
                        if (angular.isDefined($scope.autoSelectIndex) && clients && clients.length) {
                            $scope.ngModel = clients[$scope.autoSelectIndex];
                        }

                        $scope.clients = clients;
                    }).catch(function() {
                        console.error('could not load clients');
                    });
                }
            ]
        };
    }
]);