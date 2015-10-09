truckModule.directive('truckStatusReportController', [
    '$scope',
    'truckService',
    'truckStatus',
    function($scope, truckService, truckStatus) {
        $scope.truckStatus = truckStatus;

        $scope.trucks = [];
        $scope.summary = {};

        $scope.loadReport = function() {
            truckService.getCurrentStatus().then(function(trucks) {
                $scope.trucks = trucks;
                $scope.summary = truckService.calculateReportSummary(trucks);
            });
        };

        $scope.getStatusClass = function(truck) {
            return truckStatus[truck.Status].cssClass;
        };

        $scope.loadReport();
    }
]);
