tripModule.controller('costAdjustmentController', [
    '$scope',
    '$window',
    'tripService',
    'TripCost',
    '$timeout',
    'globalMessage',
    '$routeParams',
    function orderTripController(
        $scope,
        $window,
        tripService,
        TripCost,
        $timeout,
        globalMessage,
        $routeParams) {

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
            $scope.loading = false;
        });

        function save() {
            globalMessage.info('Saving cost adjustments...', 0);
            return tripService.saveAdjustment($scope.trip, $scope.costs).then(function () {
                globalMessage.success('Cost adjustments saved');
            }).catch(function (response) {
                globalMessage.error(response.Message);
            });
        }

        $scope.save = function () {
            $timeout(function() {
                if ($scope.saveAndArchive) {
                    var confirm = $window.confirm('You cannot adjust the cost after a trip is archived. \nAre you sure you want to archive?');

                    confirm && save().then(function () {
                        tripService.archive($scope.trip).then(function () {
                            globalMessage.success('Trip archived');
                        }).catch(function (response) {
                            globalMessage.error(response.Message);
                        });
                    });
                } else {
                    save();
                }
                $scope.saveAndArchive = false;
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
    }
]);