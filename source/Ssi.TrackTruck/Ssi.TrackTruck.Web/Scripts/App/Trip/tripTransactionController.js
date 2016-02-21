tripModule.controller('tripTransactionController', [
    '$scope',
    '$filter',
    'tripService',
    'collection',
    'tripStatus',
    'globalMessage',
    function (
        $scope
        , $filter
        , tripService
        , collection
        , tripStatus
        , globalMessage
        ) {

        $scope.updateStatus = function (trip) {
            trip.waiting = true;
            tripService.updateStatus(trip).then(function () {
                trip.waiting = false;
                globalMessage.success('trip status updated');
            });
        };

        $scope.filter = {
            Status: {
                New: true,
                InProgress: true,
                Delivered: true
            }
        };

        $scope.applyFilter = function () {
            var filteredTrips = $scope.trips;

            filteredTrips = $filter('filter')(filteredTrips, { TripTicketNumber: $scope.filter.TripTicketNumber });

            filteredTrips = $filter('filter')(filteredTrips, { ClientId: $scope.filter.ClientId });

            filteredTrips = $filter('filter')(filteredTrips, function (trip) {
                return $scope.filter.Status[trip.Status];
            });

            $scope.trips.forEach(function (trip) {
                trip.show = false;
            });

            filteredTrips.forEach(function (trip) {
                trip.show = true;
            });
        };

        $scope.statusButtonClass = function (status) {
            return $scope.filter.Status[status.id] ? status.cssClass : 'default';
        };

        $scope.tripStatus = tripStatus;

        tripService.getActiveTrips().then(function (trips) {
            $scope.trips = trips;
            if (trips.length) {
                trips[0].accordionOpen = true;
            }

            $scope.applyFilter();
        });

        $scope.totalRejected = function (drop) {
            return collection.sum(drop.DeliveryReceipts, 'RejectedNumberOfBoxes');
        };

        $scope.totalAccepted = function (drop) {
            return drop.TotalBoxes - $scope.totalRejected(drop);
        };

        $scope.receive = function (drop, trip) {
            tripService.receiveDrop(drop, trip).then(function () {
                drop.IsDelivered = true;
                globalMessage.success('drop received');
            }).catch(function (message) {
                globalMessage.error(message);
            }).finally(function () {
                $scope.applyFilter();
            });
        };

        $scope.addNewAdjustment = function (trip) {
            trip.Adjustments = trip.Adjustments || [];
            trip.Adjustments.push({});
        };

        $scope.saveAdjustment = function (trip) {
            tripService.saveAdjustment(trip);
        };

        $scope.deleteAdjustment = function(trip, adjustmentIndex) {
            trip.Adjustments.splice(adjustmentIndex, 1);
        };
    }
]);