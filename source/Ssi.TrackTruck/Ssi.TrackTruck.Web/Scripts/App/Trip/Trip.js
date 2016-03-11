tripModule.factory('Trip', [
    '_',
    'collection',
    'clientService',
    'employeeService',
    'userService',
    'tripStatus',
    'truckService',
    function (
        _,
        collection,
        clientService,
        employeeService,
        userService,
        tripStatus,
        truckService
        ) {
        function Trip(dbTrip, dbDrops) {
            var _this = this;
            angular.extend(_this, dbTrip);
            _this.Drops = dbDrops;
            _this.StatusObject = tripStatus[_this.Status];
            _this.TotalNumberOfBoxes = collection.sum(_this.Drops, 'TotalBoxes');
            _this.RejectedNumberOfBoxes = collection.sum(_this.Drops, 'TotalRejectedBoxes');
            _this.DeliveredNumberOfBoxes = collection.sum(_this.Drops, 'TotalDeliveredBoxes');
            _this.TotalNumberOfDrops = dbDrops.length;
            _this.DeliveredNumberOfDrops = collection.count(_this.Drops, 'IsDelivered', true);

            _this.Client = {};

            clientService.get(_this.ClientId).then(function (client) {
                _this.Client = client;
                _this.PickupAddress = _.find(_this.Client.Addresses, { Id: _this.PickupAddressId });

                userService.getIndexedUsers().then(function (userIndex) {
                    _this.Drops.forEach(function (drop) {
                        drop.Branch = _.find(_this.Client.Branches, { Id: drop.BranchId });
                        drop.ReceiverUser = userIndex[drop.ReceiverUserId];
                    });
                });
            });

            _this.Driver = {};
            employeeService.getIndexedEmployees().then(function (employeesById) {
                _this.Driver = employeesById[_this.DriverId];
                _this.Helper1 = employeesById[_this.Helper1Id];
                _this.Helper2 = employeesById[_this.Helper2Id];
                _this.Helper3 = employeesById[_this.Helper3Id];

                _this.HelperNames = _this.Helper1.FullName;
                if (_this.Helper2) {
                    _this.HelperNames += ',' + _this.Helper2.FullName;
                }
                if (_this.Helper3) {
                    _this.HelperNames += ',' + _this.Helper3.FullName;
                }

                _this.Supervisor = employeesById[_this.SupervisorId];
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