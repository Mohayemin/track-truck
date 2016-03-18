tripModule.controller('costAdjustmentController', [
    '$scope',
    'tripService',
    'globalMessage',
    '$routeParams',
    function orderTripController(
        $scope,
        tripService,
        globalMessage,
        $routeParams) {

        
        tripService.get($routeParams['id']).then(function (trip) {
            $scope.trip = trip;
            $scope.adjustments = trip.Costs.map(function(cost) {
                return {
                    Id: cost.Id,
                    ExpectedCostInPeso: cost.ExpectedCostInPeso,
                    ActualCostInPesso: cost.ActualCostInPesso,
                    CostType: cost.CostType
                }
            });
        });

        $scope.save = function() {
            tripService.saveAdjustment($scope.trip, $scope.adjustments);
        };

        $scope.addNewAdjustment = function () {
            $scope.adjustments.push({});
        };

        $scope.deleteAdjustment = function (adjustment) {
            _.remove($scope.adjustments, function(a) {
                return adjustment === a;
            });
        };
    }
]);