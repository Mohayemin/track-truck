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

        function setContractEmployee(contract, employeeIndex, contracts) {
            contract.Employee = employeeIndex[contract.EmployeeId];
            contracts.push(contract);
        }

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
                _this.Contracts = [];

                setContractEmployee(_this.DriverContract, employeesById, _this.Contracts);
                _this.HelperContracts.forEach(function(helperContract) {
                    setContractEmployee(helperContract, employeesById, _this.Contracts);
                });

                _this.HelperNames = _this.HelperContracts.map(function(helperContract) {
                    return helperContract.Employee.FullName;
                }).join(', ');

                setContractEmployee(_this.SupervisorContract, employeesById, _this.Contracts);
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