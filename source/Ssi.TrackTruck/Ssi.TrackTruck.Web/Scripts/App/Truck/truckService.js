truckModule.factory('truckService', [
    'repository',
    'buildIdMap',
    '$q',
    'employeeService',
    function truckService(repository
        , buildIdMap
        , $q
        , employeeService) {

        var _trucks = [];
        var _trucksById = [];
        var _loadPromise = null;

        function calculateReportSummary(trucks) {
            var summary = {
                trucks: { total: trucks.length },
                items: {}
            };

            [0, 1001, 1002, 1003].forEach(function(status) {
                var trucksWithStatus = _.filter(trucks, { Status: status });
                summary.trucks[status] = trucksWithStatus.length;
                summary.items[status] = trucksWithStatus.reduce(function(memo, truck) {
                    return memo + truck.ItemsCarrying;
                }, 0);
            });

            summary.items.total = trucks.reduce(function(memo, truck) {
                return memo + truck.ItemsCarrying;
            }, 0);

            return summary;
        }

        function getCurrentStatus() {
            return repository.get('Truck', 'GetCurrentStatus');
        }

        function add(request) {
            var formattedRequest = {
                RegistrationNumber: request.RegistrationNumber,
                DriverId: request.driver.Id,
                HelperId: request.helper.Id
            };
            return repository.post('Truck', 'Add', formattedRequest).then(function(response) {
                if (response.IsError) {
                    return $q.reject(response.Message);
                }
                return response;
            });
        }

        function getAll(forse) {
            if (!_loadPromise || forse) {
                _loadPromise = repository.get('Truck', 'All').then(function (trucks) {
                    employeeService.getIndexedEmployees().then(function (employees) {
                        trucks.forEach(function (truck) {
                            truck.DriverName = (employees[truck.DriverId] || {}).FullName;
                            truck.HelperName = (employees[truck.HelperId] || {}).FullName;
                        });
                    });

                    _trucks.length = 0;
                    _trucks.push.apply(_trucks, trucks);
                    _trucksById = buildIdMap(_trucks);

                    return _trucks;
                });
            }
            return _loadPromise;
        }

        var service = {
            calculateReportSummary: calculateReportSummary,
            getCurrentStatus: getCurrentStatus,
            add: add,
            getAll: getAll,
            get: function(id) {
                return getAll().then(function () {
                    return _.find(_trucks, { Id: id });
                });
            },
            getIndexedTrucks: function() {
                return service.getAll().then(function() {
                    return _trucksById;
                });
            },
            'delete': function (id) {
                return repository.post('Truck', 'Delete', { id: id }).then(function (response) {
                    if (!response.IsError) {
                        var index = _.findIndex(_trucks, { Id: id });
                        if (index >= 0) {
                            _trucks.splice(index, 1);
                            delete _trucksById[id];
                        }
                        return response;
                    }

                    return $q.reject(response.Message || response.status || 'could not delete client');
                });
            },
            edit: function(request) {
                var formattedRequest = {
                    Id: request.Id,
                    RegistrationNumber: request.RegistrationNumber,
                    DriverId: request.driver.Id,
                    HelperId: request.helper.Id
                };
                return repository.post('Truck', 'Save', formattedRequest).then(function(response) {
                    if (response.IsError) {
                        return $q.reject(response.Message || response.Status || 'Could not edit truck');
                    }

                    return employeeService.get(response.Data.DriverId).then(function(driver) {
                        response.Data.DriverName = driver.FullName;
                        employeeService.get(response.Data.HelperId).then(function(helper) {
                            response.Data.HelperName = helper.FullName;
                            angular.extend(_trucksById[request.Id], response.Data);
                        });
                        return _trucksById[request.Id];
                    });
                });
            }
        };

        return service;
    }
]);

// Map with Ssi.TrackTruck.Bussiness.DAL.Constants.TripStatus
truckModule.factory('truckStatus', [function truckStatusFactory() {
    function obj(id, cssClass, text) {
        return {
            id: id,
            cssClass: cssClass,
            text: text
        };
    }

    var factory = {
        0: obj(0, 'danger', 'Not In Use'),
        1001: obj(1001, 'success', 'Moving'),
        1002: obj(1002, 'info', 'Loading'),
        1003: obj(1003, 'warning', 'Unloading')
    };

    factory.notInUse = factory[0];
    factory.moving = factory[1001];
    factory.loading = factory[1002];
    factory.unloading = factory[1003];

    return factory;
}]);