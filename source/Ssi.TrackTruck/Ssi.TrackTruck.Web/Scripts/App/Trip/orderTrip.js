tripModule.directive('orderTrip', [
    'url',
    function (url) {
        return {
            templateUrl: url.template('Trip', 'orderTrip'),
            scope: {},
            controller: [
                '$scope',
                'clientService',
                'truckService',
                'tripService',
                'warehouseService',
                'employeeService',
                'designation',
                'globalMessage',
                function orderTripController(
                    $scope,
                    clientService,
                    truckService,
                    tripService,
                    warehouseService,
                    employeeService,
                    designation,
                    globalMessage) {

                    $scope.request = {
                        ExpectedPickupTime: { year: 2015, month: 6, day: 15, hour: 13, minute: 10 },
                        Drops: [],
                        HelperIds: [undefined, undefined, undefined],
                        FuelCost: 0,
                        TollCost: 0,
                        ParkingCost: 0,
                        BargeCost: 0,
                        BundleCost: 0,
                        HelperAllowance: 65,
                        HelperSalary: 35,
                        DriverAllowance: 65,
                        DriverSalary: 35,
                        TripTicketNumber: 'TT-' + Math.round(Math.random() * 1000)
                    };

                    $scope.addDr = function (drop) {
                        drop.DeliveryReceipts.push({});
                    };

                    $scope.addDrop = function () {
                        var drop = {
                            BranchId: null,
                            ExpectedDropTime: {},
                            DeliveryReceipts: []
                        };
                        $scope.addDr(drop);
                        $scope.request.Drops.push(drop);
                    };

                    $scope.addDrop();

                    $scope.getTotalBoxes = function (drop) {
                        return drop.DeliveryReceipts.map(function (dr) {
                            return dr.NumberOfBoxes;
                        }).reduce(function (oldV, v) {
                            return isNaN(v) ? oldV : (v + oldV);
                        }, 0);
                    };

                    clientService.getAll().then(function (clients) {
                        $scope.clients = clients;
                        $scope.request.Client = clients[clients.length - 1];
                        $scope.request.PickupAddress = $scope.request.Client.Addresses[0];

                        var drop = $scope.request.Drops[0];
                        drop.BranchId = $scope.request.Client.Branches[0].Id;
                        angular.extend(drop.ExpectedDropTime, { year: 2015, month: 9, day: 3, hour: 18, minute: 36 });
                        drop.DeliveryReceipts[0].DrNumber = "ER-234";
                        drop.DeliveryReceipts[0].NumberOfBoxes = 4;

                    }).catch(function () {
                        console.error('could not load clients');
                    });

                    truckService.getAll().then(function (trucks) {
                        $scope.trucks = trucks;
                        $scope.request.Truck = trucks[0];
                    });

                    employeeService.getAllByDesignation(designation.driver).then(function (drivers) {
                        $scope.drivers = drivers;
                        $scope.request.DriverId = drivers[0].Id;
                    });
                    employeeService.getAllByDesignation(designation.helper).then(function (helpers) {
                        $scope.helpers = helpers;
                        $scope.request.HelperIds[0] = helpers[0].Id;
                    });
                    employeeService.getAllByDesignation(designation.supervisor).then(function (supervisors) {
                        $scope.supervisors = supervisors;
                        $scope.request.SupervisorId = supervisors[0].Id;
                    });

                    $scope.$watch('request.Truck', function () {
                        var truck = $scope.request.Truck;
                        if (truck) {
                            $scope.request.DriverId = truck.DriverId || $scope.request.DriverId;
                            $scope.request.HelperId = truck.HelperId || $scope.request.HelperId;
                        }
                    });

                    $scope.order = function () {
                        globalMessage.info('Creating Order...', 0);
                        tripService.orderTrip($scope.request).then(function () {
                            globalMessage.success('Order Created.');
                        }).catch(function () {
                            globalMessage.error('Could not create order, please try again.');
                        });
                    };
                }
            ]
        }
    }
]);