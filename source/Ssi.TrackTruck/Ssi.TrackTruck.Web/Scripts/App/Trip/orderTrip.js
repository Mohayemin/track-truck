tripModule.directive('orderTrip', [
    'url',
    function(url) {
        return {
            templateUrl: url.template('Trip', 'orderTrip'),
            scope: {},
            controller: [
                '$scope',
                'clientService',
                'tripService',
                'warehouseService',
                'employeeService',
                'designation',
                'globalMessage',
                function orderTripController($scope, clientService, tripService, warehouseService, employeeService, designation, globalMessage) {
                    $scope.request = {
                        DeliveryHour: 15,
                        DeliveryMinute: 30,
                        ExpectedPickupTime: {},
                        Drops: []
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

                    warehouseService.getAll().then(function(warehouses) {
                        $scope.warehouses = warehouses;
                    }).catch(function() {
                        console.error('could not load warehouses');
                    });

                    employeeService.getTruckEmployees().then(function(employeeGroups) {
                        $scope.drivers = employeeGroups[designation.driver];
                        $scope.helpers = employeeGroups[designation.helper];
                    }).catch(function() {
                        console.error('could not load drivers and helpers');
                    });

                    $scope.order = function() {
                        globalMessage.info('Creating Order...');
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