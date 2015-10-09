truckModule.controller('truckListController', [
    '$scope',
    'truckService',
    'url',
    function ($scope
        , truckService
        , url) {
        $scope.url = url;

        truckService.getAll().then(function (trucks) {
            $scope.trucks = trucks;
        });
    }
]);