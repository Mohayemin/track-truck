truckModule.directive('truckStatusReportController', [
    '$scope',
    'truckService',
    'truckStatus',
    function($scope, truckService, truckStatus) {
        $scope.truckStatus = truckStatus;

        $scope.trucks = [];
        $scope.summary = {};

        $scope.getStatusClass = function(truck) {
            return truckStatus[truck.Status].cssClass;
        };

        $scope.loadReport();
    }
]);
