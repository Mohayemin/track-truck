tripModule.directive('orderTrip', [
    'url',
    function(url) {
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
                        ExpectedPickupTime: {},
                        Drops: [],
                        HelperIds: [undefined, undefined, undefined]
                    };

                    $scope.addDr = function(drop) {
                        drop.DeliveryReceipts.push({});
                    };

                    $scope.addDrop = function() {
                        var drop = {
                            BranchId: null,
                            ExpectedDropTime: {},
                            DeliveryReceipts: []
                        };
                        $scope.addDr(drop);
                        $scope.request.Drops.push(drop);
                    };

                    $scope.addDrop();

                    $scope.getTotalBoxes = function(drop) {
                        return drop.DeliveryReceipts.map(function(dr) {
                            return dr.NumberOfBoxes;
                        }).reduce(function(oldV, v) {
                            return isNaN(v) ? oldV : (v + oldV);
                        }, 0);
                    };

                    clientService.getAll().then(function(clients) {
                        $scope.clients = clients;
                    }).catch(function() {
                        console.error('could not load clients');
                    });

                    truckService.getAll().then(function(trucks) {
                        $scope.trucks = trucks;
                    });

                    employeeService.getAllByDesignation(designation.driver).then(function(drivers) {
                        $scope.drivers = drivers;
                    });
                    employeeService.getAllByDesignation(designation.helper).then(function (helpers) {
                        $scope.helpers = helpers;
                    });
                    employeeService.getAllByDesignation(designation.supervisor).then(function (supervisors) {
                        $scope.supervisors = supervisors;
                    });

                    $scope.$watch('request.Truck', function () {
                        var truck = $scope.request.Truck;
                        if (truck) {
                            $scope.request.DriverId = truck.DriverId || $scope.request.DriverId;
                            $scope.request.HelperId = truck.HelperId || $scope.request.HelperId;
                        }
                    });

                    $scope.order = function() {
                        globalMessage.info('Creating Order...', 0);
                        tripService.orderTrip($scope.request).then(function() {
                            globalMessage.success('Order Created.');
                        }).catch(function() {
                            globalMessage.error('Could not create order, please try again.');
                        });
                    };
                }
            ]
        }
    }
]);