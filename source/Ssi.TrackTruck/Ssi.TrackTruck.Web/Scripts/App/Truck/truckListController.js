truckModule.controller('truckListController', [
    '$scope',
    'truckService',
    function ($scope
        , truckService
        ) {

        truckService.getAll().then(function (trucks) {
            $scope.trucks = trucks;
        });
    }
]);