tripModule.factory('Trip', [
    '_',
    'collection',
    'clientService',
    'employeeService',
    'truckService',
    function (
        _,
        collection,
        clientService,
        employeeService,
        truckService
        ) {
        function Trip(dbTrip, dbDrops) {
            var _this = this;
            angular.extend(_this, dbTrip);
            _this.Drops = dbDrops;
            _this.TotalNumberOfBoxes = collection.sum(_this.Drops, 'TotalBoxes');
            _this.RejectedNumberOfBoxes = collection.sum(_this.Drops, 'TotalRejectedBoxes');
            _this.DeliveredNumberOfBoxes = collection.sum(_this.Drops, 'TotalDeliveredBoxes');
            _this.TotalNumberOfDrops = dbDrops.length;
            _this.DeliveredNumberOfDrops = collection.count(_this.Drops, 'IsReceived', true);

            _this.Client = {};
            clientService.get(_this.ClientId).then(function (client) {
                _this.Client = client;
                _this.PickupAddress = _.find(_this.Client.Addresses, { Id: _this.PickupAddressId });

                _this.Drops.forEach(function (drop) {
                    drop.Branch = _.find(_this.Client.Branches, { Id: drop.BranchId });
                });
            });

            _this.Driver = {};
            _this.Helpers = [];
            employeeService.getIndexedEmployees().then(function (employeesById) {
                _this.Driver = employeesById[_this.DriverId];
                _this.Helpers = _this.HelperIds.map(function (hid) {
                    return employeesById[hid];
                });
            });

            _this.Truck = {};
            truckService.get(_this.TruckId).then(function (truck) {
                _this.Truck = truck;
            });

            _this.AllDropsDelivered = _this.TotalNumberOfDrops == _this.DeliveredNumberOfDrops;
        }

        return Trip;
    }
]);