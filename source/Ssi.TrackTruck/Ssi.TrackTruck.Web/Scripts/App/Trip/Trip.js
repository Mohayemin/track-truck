tripModule.factory('Trip', [
    '_',
    'collection',
    'clientService',
    'employeeService',
    'userService',
    'truckService',
    function (
        _,
        collection,
        clientService,
        employeeService,
        userService,
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
            _this.Helpers = [];
            employeeService.getIndexedEmployees().then(function (employeesById) {
                _this.Driver = employeesById[_this.DriverId];
                _this.Helpers = _this.HelperIds.map(function (hid) {
                    return employeesById[hid];
                });

                _this.HelperNames = _this.Helpers.map(function(helper) {
                    return helper.FullName;
                }).join(', ');

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