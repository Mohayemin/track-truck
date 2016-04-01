tripModule.controller('costAdjustmentController', [
    '$scope',
    '$window',
    'tripService',
    'costType',
    'TripCost',
    'designation',
    'globalMessage',
    '$routeParams',
    function orderTripController(
        $scope,
        $window,
        tripService,
        costType,
        TripCost,
        designation,
        globalMessage,
        $routeParams) {

        $scope.disable = true;
        $scope.loading = true;
        tripService.get($routeParams['id']).then(function (trip) {
            $scope.trip = trip;
            $scope.costs = trip.Costs.map(function (cost) {
                return new TripCost(cost);
            });

            $scope.total = new TripCost({
                ExpectedCostInPeso: _.sumBy($scope.costs, function (a) { return a.ExpectedCostInPeso; })
            });

            $scope.recalculateTotal();
            $scope.disable = !trip.isCostAdjustable();
            $scope.loading = false;
        });

        $scope.save = function () {
            globalMessage.info('Saving cost adjustments...', 0);
            return tripService.saveAdjustment($scope.trip, $scope.costs).then(function() {
                globalMessage.success('Cost adjustments saved');
            }).catch(function (response) {
                globalMessage.error(response.Message);
            });
        };

        $scope.addNewCost = function () {
            $scope.costs.push(new TripCost({}));
        };

        $scope.deleteCost = function (cost) {
            _.remove($scope.costs, function (c) {
                return cost === c;
            });
            $scope.recalculateTotal();
        };

        $scope.recalculateTotal = function () {
            $scope.total.ActualCostInPeso = _.sumBy($scope.costs, function (a) { return a.ActualCostInPeso; });
        };

        $scope.saveAndArchive = function () {
            var confirm = $window.confirm('You cannot adjust the cost when a trip is archive. Are you sure you want to archive?');

            confirm && $scope.save().then(function() {
                tripService.archive($scope.trip).then(function () {
                    globalMessage.success('Trip archived');
                }).catch(function (response) {
                    globalMessage.error(response.Message);
                });
            });
        };
    }
]);