tripModule.factory('Trip', [
    '_',
    'collection',
    'clientService',
    'employeeService',
    'userService',
    'tripStatus',
    'TripCost',
    'truckService',
    function (
        _,
        collection,
        clientService,
        employeeService,
        userService,
        tripStatus,
        TripCost,
        truckService
        ) {
        
        function Trip(tripResponse) {
            var _this = this;
            angular.extend(_this, tripResponse.Trip);
            _this.Drops = tripResponse.Drops;
            _this.Contracts = tripResponse.Contracts;
            _this.StatusObject = tripStatus[_this.Status];
            _this.TotalNumberOfBoxes = collection.sum(_this.Drops, 'TotalBoxes');
            _this.RejectedNumberOfBoxes = collection.sum(_this.Drops, 'TotalRejectedBoxes');
            _this.DeliveredNumberOfBoxes = collection.sum(_this.Drops, 'TotalDeliveredBoxes');
            _this.TotalNumberOfDrops = _this.Drops.length;
            _this.DeliveredNumberOfDrops = collection.count(_this.Drops, 'IsDelivered', true);

            _this.Client = {};

            var userPromise = userService.getIndexedUsers();

            clientService.get(_this.ClientId).then(function (client) {
                _this.Client = client;
                _this.PickupAddress = _.find(_this.Client.Addresses, { Id: _this.PickupAddressId });
                
                userPromise.then(function (userIndex) {
                    _this.Drops.forEach(function (drop) {
                        drop.Branch = _.find(_this.Client.Branches, { Id: drop.BranchId });
                        drop.ReceiverUser = userIndex[drop.ReceiverUserId];
                    });
                });
            });

            employeeService.getIndexedEmployees().then(function (employeesById) {
                _this.Contracts.forEach(function (contract) {
                    contract.Employee = employeesById[contract.EmployeeId];
                });
            });

            _this.Truck = {};
            truckService.get(_this.TruckId).then(function (truck) {
                _this.Truck = truck;
            });

            _this.Costs = this.Costs.map(function(cost) {
                return new TripCost(cost);
            });

            userPromise.then(function(indexedUsers) {
                _this.UpdateHistories.forEach(function(history) {
                    history.User = indexedUsers[history.UserId];
                });
            });

            _this.AllDropsDelivered = _this.TotalNumberOfDrops === _this.DeliveredNumberOfDrops;
        }

        Trip.prototype.isArchived = function() {
            return this.Status === tripStatus.Archived.id;
        };

        Trip.prototype.isInProgress = function () {
            return this.Status === tripStatus.InProgress.id;
        };

        Trip.prototype.isDelivered = function () {
            return this.Status === tripStatus.Delivered.id;
        };

        Trip.prototype.isNew = function() {
            return this.Status === tripStatus.New.id;
        };

        Trip.prototype.isCostAdjustable = function() {
            return this.isNew() || this.isDelivered();
        };

        return Trip;
    }
]);