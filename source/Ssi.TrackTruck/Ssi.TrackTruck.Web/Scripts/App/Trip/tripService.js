tripModule.factory('tripService', [
    'repository',
    'signedInUser',
    'buildIdMap',
    '_',
    'clientService',
    'employeeService',
    'truckService',
    'collection',
    '$q',
    function tripService(
        repository
        , signedInUser
        , buildIdMap
        , _
        , clientService
        , employeeService
        , truckService
        , collection
        , $q
        ) {
        
        var service = {
            orderTrip: function (request) {
                var foramtterRequest = angular.extend({}, request);

                ['Client', 'Truck'].forEach(function (prop) {
                    foramtterRequest[prop + "Id"] = request[prop].Id;
                    delete foramtterRequest[prop];
                });

                return repository.post('Trip', 'Order', foramtterRequest).then(function(response) {
                    if (response.IsError) {
                        return $q.reject(response.Message);
                    }
                    return response;
                });
            },
            getMyActiveDrops: function () {
                return repository.get('Trip', 'MyActiveDrops');
            },
            receiveDrop: function (drop) {
                var formattedRequest = {
                    DropId: drop.Id,
                    DeliveryRejections: {}
                };

                drop.DeliveryReceipts.forEach(function (dr) {
                    formattedRequest.DeliveryRejections[dr.Id] = dr.RejectedNumberOfBoxes;
                });

                return repository.post('Trip', 'Receive', formattedRequest).then(function (response) {
                    if (response.IsError) {
                        return $q.reject(response.Message);
                    }
                    return response;
                });
            },
            getReport: function (filter) {
                return repository.post('Trip', 'Report', filter).then(function (report) {
                    return $q.all([clientService.getIndexedClients()
                        , employeeService.getIndexedEmployees()
                        , truckService.getIndexedTrucks()
                    , truckService])
                        .then(function (lists) {
                            var clientsById = lists[0];
                            var employeesById = lists[1];
                            var trucksById = lists[2];

                            report.Trips.forEach(function (trip) {
                                var drops = _.where(report.Drops, { TripId: trip.Id });
                                trip.Client = clientsById[trip.ClientId];
                                trip.Driver = employeesById[trip.DriverId];
                                trip.HelperNames = trip.HelperIds.map(function (hid) {
                                    return (employeesById[hid] || {}).FullName;
                                }).join(', ');
                                trip.Truck = trucksById[trip.TruckId];
                                trip.TotalNumberOfBoxes = collection.sum(drops, 'TotalBoxes');
                                trip.RejectedNumberOfBoxes = collection.sum(drops, 'TotalRejectedBoxes');
                                trip.DeliveredNumberOfBoxes = collection.sum(drops, 'TotalDeliveredBoxes');

                                var pickupAddress = _.find(trip.Client.Addresses, { Id: trip.PickupAddressId });
                                var routeList = [pickupAddress.Text];

                                drops.forEach(function (drop) {
                                    return routeList.push(_.find(trip.Client.Branches, { Id: drop.BranchId }).Name);
                                });

                                trip.Route = routeList.join(', ');
                            });

                            return report.Trips;
                        });
                });
            }
        };

        return service;
    }
]);
