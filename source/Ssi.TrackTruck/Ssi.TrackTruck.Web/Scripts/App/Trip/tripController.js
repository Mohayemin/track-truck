trackTruck.controller('tripController', [
    '$scope',
    'tripService',
    tripController
]);

function tripController($scope, tripService) {
    $scope.addRequest = {};
    $scope.addTrip = function() {
        tripService.addTrip($scope.addRequest).then(function () {
            
        }).catch(function() {
            
        });
    };
}