truckModule.directive('truckList', [
    'url',
    function(url) {
        return {
            templateUrl: url.template('Truck', 'truckList'),
            scope: {},
            controller: [
                '$scope',
                'truckService',
                function($scope,
                    truckService) {

                    $scope.url = url;

                    truckService.getAll().then(function(trucks) {
                        $scope.trucks = trucks;
                    });
                }
            ]
        };
    }
]);