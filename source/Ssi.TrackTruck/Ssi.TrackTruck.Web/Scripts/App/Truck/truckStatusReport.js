trackTruck.directive('truckStatusReport', [
    'url',
    'truckService',
    'truckStatus',
    truckStatusReportDirective
]);

function truckStatusReportDirective(url, truckService, truckStatus) {
    return {
        templateUrl: url.resolveTemplate('Truck', 'truckStatusReport'),
        scope: {},
        controller: [
            '$scope',
            function ($scope) {
                $scope.truckStatus = truckStatus;

                $scope.trucks = [];
                $scope.summary = {};

                $scope.loadReport = function () {
                    truckService.getCurrentStatus().then(function (trucks) {
                        $scope.trucks = trucks;
                        $scope.summary = truckService.calculateReportSummary(trucks);
                    });
                };

                $scope.getStatusClass = function (truck) {
                    return truckStatus[truck.Status].cssClass;
                };

                $scope.loadReport();
            }
        ]
    };
}