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

                    truckService.getAll().then(function(trucks) {
                        $scope.trucks = trucks;
                    });
                }
            ]
        };
    }
]);