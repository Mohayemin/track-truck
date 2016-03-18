﻿tripModule.controller('costAdjustmentController', [
    '$scope',
    'tripService',
    'costType',
    'TripCost',
    'globalMessage',
    '$routeParams',
    function orderTripController(
        $scope,
        tripService,
        costType,
        TripCost,
        globalMessage,
        $routeParams) {
        
        tripService.get($routeParams['id']).then(function (trip) {
            $scope.trip = trip;
            $scope.costs = trip.Costs.map(function (cost) {
                return new TripCost(cost);
            });
            $scope.total = new TripCost({
                ExpectedCostInPeso: _.sumBy($scope.costs, function (a) { return a.ExpectedCostInPeso; })
            });

            $scope.recalculateTotal();
        });

        $scope.save = function () {
            tripService.saveAdjustment($scope.trip, $scope.costs);
        };

        $scope.addNewCost = function () {
            $scope.costs.push(new TripCost({}));
        };

        $scope.deleteCost = function (cost) {
            _.remove($scope.costs, function (c) {
                return cost === c;
            });
        };

        $scope.recalculateTotal = function () {
            $scope.total.ActualCostInPeso = _.sumBy($scope.costs, function (a) { return a.ActualCostInPeso; });
        };
    }
]);