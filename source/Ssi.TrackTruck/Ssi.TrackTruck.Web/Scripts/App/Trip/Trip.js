﻿tripModule.factory('Trip', [
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

        function setContractEmployee(contract, employeeIndex) {
            contract.Employee = employeeIndex[contract.EmployeeId];
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
                setContractEmployee(_this.DriverContract, employeesById);
                setContractEmployee(_this.Helper1Contract, employeesById);

                _this.Contracts = [_this.DriverContract, _this.Helper1Contract];

                _this.HelperNames = employeesById[_this.Helper1Contract.EmployeeId].FullName;
                if (_this.Helper2Contract) {
                    setContractEmployee(_this.Helper2Contract, employeesById);
                    _this.HelperNames += ',' + employeesById[_this.Helper2Contract.EmployeeId].FullName;
                    _this.Contracts.push(_this.Helper2Contract);
                }
                if (_this.Helper3Contract) {
                    setContractEmployee(_this.Helper3Contract, employeesById);
                    _this.HelperNames += ',' + employeesById[_this.Helper3Contract.EmployeeId].FullName;
                    _this.Contracts.push(_this.Helper3Contract);
                }

                setContractEmployee(_this.SupervisorContract, employeesById);
                _this.Contracts.push(_this.SupervisorContract);
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