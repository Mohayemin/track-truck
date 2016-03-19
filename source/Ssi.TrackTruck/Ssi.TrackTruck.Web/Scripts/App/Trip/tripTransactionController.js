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
        
        $scope.filter = {
            Status: {
                New: true,
                InProgress: true,
                Delivered: true
            }
        };

        function applyFilter() {
            var filteredTrips = $scope.trips;

            if (!filteredTrips) {
                return;
            }
            filteredTrips = $filter('filter')(filteredTrips, { TripTicketNumber: $scope.filter.TripTicketNumber });

            if ($scope.filter.Client) {
                filteredTrips = $filter('filter')(filteredTrips, { ClientId: $scope.filter.Client.Id });
            }

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

        $scope.$watchCollection('filter', applyFilter);
        $scope.$watchCollection('filter.Status', applyFilter);

        $scope.statusButtonClass = function (status) {
            return $scope.filter.Status[status.id] ? status.cssClass : 'default';
        };

        $scope.tripStatus = tripStatus;

        tripService.getActiveTrips().then(function (trips) {
            $scope.trips = trips;
            if (trips.length) {
                trips[0].accordionOpen = true;
            }

            applyFilter();
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
                applyFilter();
            });
        };


    }
]);