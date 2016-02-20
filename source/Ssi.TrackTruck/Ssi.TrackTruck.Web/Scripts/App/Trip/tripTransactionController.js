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

        var allTrips = [];
        $scope.trips = [];

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
                InProgress: true
            }
        };

        $scope.applyFilter = function () {
            var filteredTrips = allTrips;
            filteredTrips = $filter('filter')(filteredTrips, { TripTicketNumber: $scope.filter.TripTicketNumber });
            filteredTrips = $filter('filter')(filteredTrips, { ClientId: $scope.filter.ClientId });
            filteredTrips = $filter('filter')(filteredTrips, function(trip) {
                return $scope.filter.Status[trip.Status];
            });

            $scope.trips = filteredTrips;
        };

        $scope.statusButtonClass = function (status) {
            return $scope.filter.Status[status.id] ? status.cssClass : 'default';
        };

        $scope.tripStatus = tripStatus;

        tripService.getActiveTrips().then(function (trips) {
            allTrips = trips;
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
            }).finally(function() {
                $scope.applyFilter();
            });
        };
    }
]);