tripModule.controller('costAdjustmentController', [
    '$scope',
    'tripService',
    'costType',
    'globalMessage',
    '$routeParams',
    function orderTripController(
        $scope,
        tripService,
        costType,
        globalMessage,
        $routeParams) {

        function Adjustment(cost) {
            this.Id = cost.Id;
            this.ExpectedCostInPeso = cost.ExpectedCostInPeso || 0;
            this.ActualCostInPeso = cost.ActualCostInPeso || this.ExpectedCostInPeso;
            this.CostType = cost.CostType || costType.Discrepancy;
        }

        Adjustment.prototype.getAdjustment = function() {
            return this.ExpectedCostInPeso - this.ActualCostInPeso;
        };

        
        tripService.get($routeParams['id']).then(function (trip) {
            $scope.trip = trip;
            $scope.adjustments = trip.Costs.map(function (cost) {
                return new Adjustment(cost);
            });
            $scope.total = new Adjustment({
                ExpectedCostInPeso: _.sumBy($scope.adjustments, function (a) { return a.ExpectedCostInPeso; })
            });

            $scope.recalculateTotal();
        });

        $scope.save = function () {
            tripService.saveAdjustment($scope.trip, $scope.adjustments);
        };

        $scope.addNewAdjustment = function () {
            $scope.adjustments.push(new Adjustment({}));
        };

        $scope.deleteAdjustment = function (adjustment) {
            _.remove($scope.adjustments, function (a) {
                return adjustment === a;
            });
        };

        $scope.recalculateTotal = function () {
            $scope.total.ActualCostInPeso = _.sumBy($scope.adjustments, function (a) { return a.ActualCostInPeso; });
        };
    }
]);