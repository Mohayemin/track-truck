trackTruck.controller('trackTruckController', [
    '$scope',
    'dateFormat',
    trackTruckController
]);

function trackTruckController($scope, dateFormat) {
    $scope.dateFormat = dateFormat;
};

