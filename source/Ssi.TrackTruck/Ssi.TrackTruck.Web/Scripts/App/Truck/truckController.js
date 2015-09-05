trackTruck.controller('truckController', [
    '$scope',
    'truckService',
    'truckStatus',
    truckController
]);

function truckController($scope, truckService, truckStatus) {
    $scope.DetailReportHeaders = {
        'TruckId': 'Truck Number',
        'DriverName': 'Driver',
        'ItemsCarrying': 'Items',
        'Status': 'Status',
        'FromOutlet': 'From Outlet',
        'ToOutlet': 'To Outlet'
    };
    $scope.trucks = [];
    $scope.summary = {};

    truckService.getCurrentStatus().then(function (trucks) {
        $scope.trucks = trucks;
        $scope.summary = truckService.calculateReportSummary(trucks);
    });
    
    $scope.getStatusClass = function (truck) {
        return truckStatus[truck.Status].cssClass;
    };

    $scope.sort = { Name: 'DriverId', Reverse: false };
    $scope.updateSort = function (headerId) {
        if ($scope.sort.Id == headerId) {
            $scope.sort.Reverse = !$scope.sort.Reverse;
        } else {
            $scope.sort.Id = headerId;
            $scope.sort.Reverse = false;
        }
    };
}